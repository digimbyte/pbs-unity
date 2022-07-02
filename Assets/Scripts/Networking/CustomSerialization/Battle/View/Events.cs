using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using PBS.Enums.Battle;

namespace PBS.Networking.CustomSerialization.Battle.View
{
    public static class Events
    {
        const int BASE = 0;

        // Battle Phases (1 - 99)
        const int STARTBATTLE = 1;
        const int ENDBATTLE = 2;


        // Messages (101 - 199)
        const int MESSAGE = 101;
        const int MESSAGEPARAMETERIZED = 102;


        // Backend (201 - 299)
        const int MODELUPDATE = 201;
        const int MODELUPDATEPOKEMON = 202;
        const int MODELUPDATETRAINER = 203;
        const int MODELUPDATETEAM = 204;

        // Command Prompts (301 -399)
        const int COMMANDGENERALPROMPT = 301;
        const int COMMANDREPLACEMENTPROMPT = 302;


        // Trainer Interactions (501 - 599)
        const int TRAINERSENDOUT = 501;
        const int TRAINERMULTISENDOUT = 502;
        const int TRAINERWITHDRAW = 503;
        const int TRAINERITEMUSE = 510;


        // Team Interactions (601 - 699)


        // Environmental Interactions (701 - 799)


        // --- Pokemon Interactions ---

        // General (1001 - 1099)
        const int POKEMONCHANGEFORM = 1005;
        const int POKEMONSWITCHPOSITION = 1050;

        // Damage / Health (1101 - 1199)
        const int POKEMONHEALTHDAMAGE = 1101;
        const int POKEMONHEALTHHEAL = 1102;
        const int POKEMONHEALTHFAINT = 1103;
        const int POKEMONHEALTHREVIVE = 1104;

        // Abilities (1201 - 1299)
        const int POKEMONABILITYACTIVATE = 1201;

        // Moves (1301 - 1399)
        const int POKEMONMOVEUSE = 1301;
        const int POKEMONMOVEHIT = 1302;

        // Stats (1401 - 1499)
        const int POKEMONSTATCHANGE = 1401;
        const int POKEMONSTATUNCHANGEABLE = 1402;


        public static void WriteBattleViewEvent(this NetworkWriter writer, PBS.Battle.View.Events.Base obj)
        {
            if (obj is PBS.Battle.View.Events.Message message)
            {
                writer.WriteInt(MESSAGE);
                writer.WriteString(message.message);
            }
            else if (obj is PBS.Battle.View.Events.MessageParameterized messageParameterized)
            {
                writer.WriteInt(MESSAGEPARAMETERIZED);
                writer.WriteString(messageParameterized.messageCode);
                writer.WriteBool(messageParameterized.isQueryResponse);
                writer.WriteBool(messageParameterized.isQuerySuccessful);
                writer.WriteInt(messageParameterized.playerPerspectiveID);
                writer.WriteInt(messageParameterized.teamPerspectiveID);

                writer.WriteString(messageParameterized.pokemonID);
                writer.WriteString(messageParameterized.pokemonUserID);
                writer.WriteString(messageParameterized.pokemonTargetID);
                writer.WriteList(messageParameterized.pokemonListIDs);

                writer.WriteInt(messageParameterized.trainerID);

                writer.WriteInt(messageParameterized.teamID);

                writer.WriteString(messageParameterized.typeID);
                writer.WriteList(messageParameterized.typeIDs);

                writer.WriteString(messageParameterized.moveID);
                writer.WriteList(messageParameterized.moveIDs);

                writer.WriteString(messageParameterized.abilityID);
                writer.WriteList(messageParameterized.abilityIDs);

                writer.WriteString(messageParameterized.itemID);
                writer.WriteList(messageParameterized.itemIDs);

                writer.WriteString(messageParameterized.statusID);
                writer.WriteString(messageParameterized.statusTeamID);
                writer.WriteString(messageParameterized.statusEnvironmentID);

                writer.WriteList(messageParameterized.intArgs);

                List<int> statInts = new List<int>();
                for (int i = 0; i < messageParameterized.statList.Count; i++)
                {
                    statInts.Add((int)messageParameterized.statList[i]);
                }
                writer.WriteList(statInts);
            }


            else if (obj is PBS.Battle.View.Events.ModelUpdate modelUpdate)
            {
                writer.WriteInt(MODELUPDATE);
                writer.WriteBool(modelUpdate.loadAssets);
            }
            else if (obj is PBS.Battle.View.Events.ModelUpdatePokemon modelUpdatePokemon)
            {
                writer.WriteInt(MODELUPDATEPOKEMON);
                writer.WriteBool(modelUpdatePokemon.loadAsset);
                writer.WriteBattleViewCompactPokemon(modelUpdatePokemon.pokemon);
            }
            else if (obj is PBS.Battle.View.Events.ModelUpdateTrainer modelUpdateTrainer)
            {
                writer.WriteInt(MODELUPDATETRAINER);
                writer.WriteBool(modelUpdateTrainer.loadAsset);
                writer.WriteString(modelUpdateTrainer.name);
                writer.WriteInt(modelUpdateTrainer.playerID);
                writer.WriteInt(modelUpdateTrainer.teamID);
                writer.WriteList(modelUpdateTrainer.party);
                writer.WriteList(modelUpdateTrainer.items);
                writer.WriteList(modelUpdateTrainer.controlPos);
            }
            else if (obj is PBS.Battle.View.Events.ModelUpdateTeam modelUpdateTeam)
            {
                writer.WriteInt(MODELUPDATETEAM);
                writer.WriteBool(modelUpdateTeam.loadAsset);
                writer.WriteInt(modelUpdateTeam.teamID);
                writer.WriteInt((int)modelUpdateTeam.teamMode);
                writer.WriteList(modelUpdateTeam.trainers);
            }


            else if (obj is PBS.Battle.View.Events.CommandGeneralPrompt commandGeneralPrompt)
            {
                writer.WriteInt(COMMANDGENERALPROMPT);
                writer.WriteInt(commandGeneralPrompt.playerID);
                writer.WriteBool(commandGeneralPrompt.multiTargetSelection);
                writer.WriteBool(commandGeneralPrompt.canMegaEvolve);
                writer.WriteBool(commandGeneralPrompt.canZMove);
                writer.WriteBool(commandGeneralPrompt.canDynamax);
                writer.WriteList(commandGeneralPrompt.items);
                writer.WriteList(commandGeneralPrompt.pokemonToCommand);
            }
            else if (obj is PBS.Battle.View.Events.CommandReplacementPrompt commandReplacementPrompt)
            {
                writer.WriteInt(COMMANDREPLACEMENTPROMPT);
                writer.WriteInt(commandReplacementPrompt.playerID);
                writer.WriteArray(commandReplacementPrompt.fillPositions);
            }


            else if (obj is PBS.Battle.View.Events.StartBattle startBattle)
            {
                writer.WriteInt(STARTBATTLE);
            }
            else if (obj is PBS.Battle.View.Events.EndBattle endBattle)
            {
                writer.WriteInt(ENDBATTLE);
                writer.WriteInt(endBattle.winningTeam);
            }


            else if (obj is PBS.Battle.View.Events.TrainerSendOut trainerSendOut)
            {
                writer.WriteInt(TRAINERSENDOUT);
                writer.WriteInt(trainerSendOut.playerID);
                writer.WriteList(trainerSendOut.pokemonUniqueIDs);
            }
            else if (obj is PBS.Battle.View.Events.TrainerMultiSendOut trainerMultiSendOut)
            {
                writer.WriteInt(TRAINERMULTISENDOUT);
                writer.WriteList(trainerMultiSendOut.sendEvents);
            }
            else if (obj is PBS.Battle.View.Events.TrainerWithdraw trainerWithdraw)
            {
                writer.WriteInt(TRAINERWITHDRAW);
                writer.WriteInt(trainerWithdraw.playerID);
                writer.WriteList(trainerWithdraw.pokemonUniqueIDs);
            }
            else if (obj is PBS.Battle.View.Events.TrainerItemUse trainerItemUse)
            {
                writer.WriteInt(TRAINERITEMUSE);
                writer.WriteInt(trainerItemUse.playerID);
                writer.WriteString(trainerItemUse.itemID);
            }


            else if (obj is PBS.Battle.View.Events.PokemonChangeForm pokemonChangeForm)
            {
                writer.WriteInt(POKEMONCHANGEFORM);
                writer.WriteString(pokemonChangeForm.pokemonUniqueID);
                writer.WriteString(pokemonChangeForm.preForm);
                writer.WriteString(pokemonChangeForm.postForm);
            }
            else if (obj is PBS.Battle.View.Events.PokemonSwitchPosition pokemonSwitchPosition)
            {
                writer.WriteInt(POKEMONSWITCHPOSITION);
                writer.WriteString(pokemonSwitchPosition.pokemonUniqueID1);
                writer.WriteString(pokemonSwitchPosition.pokemonUniqueID2);
            }

            else if (obj is PBS.Battle.View.Events.PokemonHealthDamage pokemonHealthDamage)
            {
                writer.WriteInt(POKEMONHEALTHDAMAGE);
                writer.WriteString(pokemonHealthDamage.pokemonUniqueID);
                writer.WriteInt(pokemonHealthDamage.preHP);
                writer.WriteInt(pokemonHealthDamage.postHP);
                writer.WriteInt(pokemonHealthDamage.maxHP);
            }
            else if (obj is PBS.Battle.View.Events.PokemonHealthHeal pokemonHealthHeal)
            {
                writer.WriteInt(POKEMONHEALTHHEAL);
                writer.WriteString(pokemonHealthHeal.pokemonUniqueID);
                writer.WriteInt(pokemonHealthHeal.preHP);
                writer.WriteInt(pokemonHealthHeal.postHP);
                writer.WriteInt(pokemonHealthHeal.maxHP);
            }
            else if (obj is PBS.Battle.View.Events.PokemonHealthFaint pokemonHealthFaint)
            {
                writer.WriteInt(POKEMONHEALTHFAINT);
                writer.WriteString(pokemonHealthFaint.pokemonUniqueID);
            }
            else if (obj is PBS.Battle.View.Events.PokemonHealthRevive pokemonHealthRevive)
            {
                writer.WriteInt(POKEMONHEALTHREVIVE);
                writer.WriteString(pokemonHealthRevive.pokemonUniqueID);
            }

            else if (obj is PBS.Battle.View.Events.PokemonMoveUse pokemonMoveUse)
            {
                writer.WriteInt(POKEMONMOVEUSE);
                writer.WriteString(pokemonMoveUse.pokemonUniqueID);
                writer.WriteString(pokemonMoveUse.moveID);
            }
            else if (obj is PBS.Battle.View.Events.PokemonMoveHit pokemonMoveHit)
            {
                writer.WriteInt(POKEMONMOVEHIT);
                writer.WriteString(pokemonMoveHit.pokemonUniqueID);
                writer.WriteString(pokemonMoveHit.moveID);
                writer.WriteInt(pokemonMoveHit.currentHit);
                writer.WriteList(pokemonMoveHit.hitTargets);
            }

            else if (obj is PBS.Battle.View.Events.PokemonStatChange pokemonStatChange)
            {
                writer.WriteInt(POKEMONSTATCHANGE);
                writer.WriteString(pokemonStatChange.pokemonUniqueID);
                writer.WriteInt(pokemonStatChange.modValue);
                writer.WriteBool(pokemonStatChange.maximize);
                writer.WriteBool(pokemonStatChange.minimize);

                List<int> statInts = new List<int>();
                for (int i = 0; i < pokemonStatChange.statsToMod.Count; i++)
                {
                    statInts.Add((int)pokemonStatChange.statsToMod[i]);
                }
                writer.WriteList(statInts);
            }
            else if (obj is PBS.Battle.View.Events.PokemonStatUnchangeable pokemonStatUnchangeable)
            {
                writer.WriteInt(POKEMONSTATUNCHANGEABLE);
                writer.WriteString(pokemonStatUnchangeable.pokemonUniqueID);
                writer.WriteBool(pokemonStatUnchangeable.tooHigh);

                List<int> statInts = new List<int>();
                for (int i = 0; i < pokemonStatUnchangeable.statsToMod.Count; i++)
                {
                    statInts.Add((int)pokemonStatUnchangeable.statsToMod[i]);
                }
                writer.WriteList(statInts);
            }

            else if (obj is PBS.Battle.View.Events.PokemonAbilityActivate pokemonAbilityActivate)
            {
                writer.WriteInt(POKEMONABILITYACTIVATE);
                writer.WriteString(pokemonAbilityActivate.pokemonUniqueID);
                writer.WriteString(pokemonAbilityActivate.abilityID);
            }

            else
            {
                writer.WriteInt(BASE);
            }
        }
        public static PBS.Battle.View.Events.Base ReadBattleViewEvent(this NetworkReader reader)
        {
            int type = reader.ReadInt();
            switch(type)
            {
                case STARTBATTLE:
                    return new PBS.Battle.View.Events.StartBattle
                    {

                    };
                case ENDBATTLE:
                    return new PBS.Battle.View.Events.EndBattle
                    {
                        winningTeam = reader.ReadInt()
                    };


                case MESSAGE:
                    return new PBS.Battle.View.Events.Message
                    {
                        message = reader.ReadString()
                    };
                case MESSAGEPARAMETERIZED:
                    PBS.Battle.View.Events.MessageParameterized messageParameterized = new PBS.Battle.View.Events.MessageParameterized
                    {
                        messageCode = reader.ReadString(),
                        isQueryResponse = reader.ReadBool(),
                        isQuerySuccessful = reader.ReadBool(),
                        playerPerspectiveID = reader.ReadInt(),
                        teamPerspectiveID = reader.ReadInt(),

                        pokemonID = reader.ReadString(),
                        pokemonUserID = reader.ReadString(),
                        pokemonTargetID = reader.ReadString(),
                        pokemonListIDs = reader.ReadList<string>(),

                        trainerID = reader.ReadInt(),

                        teamID = reader.ReadInt(),
                        
                        typeID = reader.ReadString(),
                        typeIDs = reader.ReadList<string>(),

                        moveID = reader.ReadString(),
                        moveIDs = reader.ReadList<string>(),

                        abilityID = reader.ReadString(),
                        abilityIDs = reader.ReadList<string>(),

                        itemID = reader.ReadString(),
                        itemIDs = reader.ReadList<string>(),

                        statusID = reader.ReadString(),
                        statusTeamID = reader.ReadString(),
                        statusEnvironmentID = reader.ReadString(),

                        intArgs = reader.ReadList<int>(),
                    };

                    List<PokemonStats> messageParameterizedStatList = new List<PokemonStats>();
                    List<int> messageParameterizedstatInts = reader.ReadList<int>();
                    for (int i = 0; i < messageParameterizedstatInts.Count; i++)
                    {
                        messageParameterizedStatList.Add((PokemonStats)messageParameterizedstatInts[i]);
                    }

                    messageParameterized.statList.AddRange(messageParameterizedStatList);
                    return messageParameterized;


                case MODELUPDATE:
                    return new PBS.Battle.View.Events.ModelUpdate
                    {
                        loadAssets = reader.ReadBool()
                    };
                case MODELUPDATEPOKEMON:
                    return new PBS.Battle.View.Events.ModelUpdatePokemon
                    {
                        loadAsset = reader.ReadBool(),
                        pokemon = reader.ReadBattleViewCompactPokemon()
                    };
                case MODELUPDATETRAINER:
                    return new PBS.Battle.View.Events.ModelUpdateTrainer
                    {
                        loadAsset = reader.ReadBool(),
                        name = reader.ReadString(),
                        playerID = reader.ReadInt(),
                        teamID = reader.ReadInt(),
                        party = reader.ReadList<string>(),
                        items = reader.ReadList<string>(),
                        controlPos = reader.ReadList<int>()
                    };
                case MODELUPDATETEAM:
                    return new PBS.Battle.View.Events.ModelUpdateTeam
                    {
                        loadAsset = reader.ReadBool(),
                        teamID = reader.ReadInt(),
                        teamMode = (TeamMode)reader.ReadInt(),
                        trainers = reader.ReadList<int>()
                    };


                case COMMANDGENERALPROMPT:
                    return new PBS.Battle.View.Events.CommandGeneralPrompt
                    {
                        playerID = reader.ReadInt(),
                        multiTargetSelection = reader.ReadBool(),
                        canMegaEvolve = reader.ReadBool(),
                        canZMove = reader.ReadBool(),
                        canDynamax = reader.ReadBool(),
                        items = reader.ReadList<string>(),
                        pokemonToCommand = reader.ReadList<PBS.Battle.View.Events.CommandAgent>()
                    };
                case COMMANDREPLACEMENTPROMPT:
                    return new PBS.Battle.View.Events.CommandReplacementPrompt
                    {
                        playerID = reader.ReadInt(),
                        fillPositions = reader.ReadArray<int>()
                    };


                case TRAINERSENDOUT:
                    return new PBS.Battle.View.Events.TrainerSendOut
                    {
                        playerID = reader.ReadInt(),
                        pokemonUniqueIDs = reader.ReadList<string>()
                    };
                case TRAINERMULTISENDOUT:
                    return new PBS.Battle.View.Events.TrainerMultiSendOut
                    {
                        sendEvents = reader.ReadList<PBS.Battle.View.Events.TrainerSendOut>()
                    };
                case TRAINERWITHDRAW:
                    return new PBS.Battle.View.Events.TrainerWithdraw
                    {
                        playerID = reader.ReadInt(),
                        pokemonUniqueIDs = reader.ReadList<string>()
                    };
                case TRAINERITEMUSE:
                    return new PBS.Battle.View.Events.TrainerItemUse
                    {
                        playerID = reader.ReadInt(),
                        itemID = reader.ReadString()
                    };


                case POKEMONCHANGEFORM:
                    return new PBS.Battle.View.Events.PokemonChangeForm
                    {
                        pokemonUniqueID = reader.ReadString(),
                        preForm = reader.ReadString(),
                        postForm = reader.ReadString()
                    };
                case POKEMONSWITCHPOSITION:
                    return new PBS.Battle.View.Events.PokemonSwitchPosition
                    {
                        pokemonUniqueID1 = reader.ReadString(),
                        pokemonUniqueID2 = reader.ReadString()
                    };

                case POKEMONHEALTHDAMAGE:
                    return new PBS.Battle.View.Events.PokemonHealthDamage
                    {
                        pokemonUniqueID = reader.ReadString(),
                        preHP = reader.ReadInt(),
                        postHP = reader.ReadInt(),
                        maxHP = reader.ReadInt()
                    };
                case POKEMONHEALTHHEAL:
                    return new PBS.Battle.View.Events.PokemonHealthHeal
                    {
                        pokemonUniqueID = reader.ReadString(),
                        preHP = reader.ReadInt(),
                        postHP = reader.ReadInt(),
                        maxHP = reader.ReadInt()
                    };
                case POKEMONHEALTHFAINT:
                    return new PBS.Battle.View.Events.PokemonHealthFaint
                    {
                        pokemonUniqueID = reader.ReadString()
                    };
                case POKEMONHEALTHREVIVE:
                    return new PBS.Battle.View.Events.PokemonHealthRevive
                    {
                        pokemonUniqueID = reader.ReadString()
                    };

                case POKEMONMOVEUSE:
                    return new PBS.Battle.View.Events.PokemonMoveUse
                    {
                        pokemonUniqueID = reader.ReadString(),
                        moveID = reader.ReadString()
                    };
                case POKEMONMOVEHIT:
                    return new PBS.Battle.View.Events.PokemonMoveHit
                    {
                        pokemonUniqueID = reader.ReadString(),
                        moveID = reader.ReadString(),
                        currentHit = reader.ReadInt(),
                        hitTargets = reader.ReadList<PBS.Battle.View.Events.PokemonMoveHitTarget>()
                    };

                case POKEMONABILITYACTIVATE:
                    return new PBS.Battle.View.Events.PokemonAbilityActivate
                    {
                        pokemonUniqueID = reader.ReadString(),
                        abilityID = reader.ReadString()
                    };

                case POKEMONSTATCHANGE:
                    PBS.Battle.View.Events.PokemonStatChange statChange = new PBS.Battle.View.Events.PokemonStatChange
                    {
                        pokemonUniqueID = reader.ReadString(),
                        modValue = reader.ReadInt(),
                        maximize = reader.ReadBool(),
                        minimize = reader.ReadBool(),
                        statsToMod = new List<PokemonStats>()
                    };
                    List<PokemonStats> statsToMod = new List<PokemonStats>();
                    List<int> statInts = reader.ReadList<int>();
                    for (int i = 0; i < statInts.Count; i++)
                    {
                        statsToMod.Add((PokemonStats)statInts[i]);
                    }
                    statChange.statsToMod.AddRange(statsToMod);
                    return statChange;

                case POKEMONSTATUNCHANGEABLE:
                    PBS.Battle.View.Events.PokemonStatUnchangeable statUnchangeable = new PBS.Battle.View.Events.PokemonStatUnchangeable
                    {
                        pokemonUniqueID = reader.ReadString(),
                        tooHigh = reader.ReadBool(),
                        statsToMod = new List<PokemonStats>()
                    };
                    List<PokemonStats> statsToModUnchangeable = new List<PokemonStats>();
                    List<int> statIntsUnchangeable = reader.ReadList<int>();
                    for (int i = 0; i < statIntsUnchangeable.Count; i++)
                    {
                        statsToModUnchangeable.Add((PokemonStats)statIntsUnchangeable[i]);
                    }
                    statUnchangeable.statsToMod.AddRange(statsToModUnchangeable);
                    return statUnchangeable;

                default:
                    throw new System.Exception($"Invalid event type {type}");
            }
        }

        public static void WriteBattleViewEventPokemonMoveHitTarget(this NetworkWriter writer, PBS.Battle.View.Events.PokemonMoveHitTarget obj)
        {
            writer.WriteString(obj.pokemonUniqueID);
            writer.WriteBool(obj.affectedByMove);
            writer.WriteBool(obj.missed);
            writer.WriteBool(obj.criticalHit);
            writer.WriteInt(obj.preHP);
            writer.WriteInt(obj.postHP);
            writer.WriteInt(obj.maxHP);
            writer.WriteInt(obj.damageDealt);
            writer.WriteDouble((double)obj.effectiveness);
        }
        public static PBS.Battle.View.Events.PokemonMoveHitTarget ReadBattleViewEventPokemonMoveHitTarget(this NetworkReader reader)
        {
            return new PBS.Battle.View.Events.PokemonMoveHitTarget
            {
                pokemonUniqueID = reader.ReadString(),
                affectedByMove = reader.ReadBool(),
                missed = reader.ReadBool(),
                criticalHit = reader.ReadBool(),
                preHP = reader.ReadInt(),
                postHP = reader.ReadInt(),
                maxHP = reader.ReadInt(),
                damageDealt = reader.ReadInt(),
                effectiveness = (float)reader.ReadDouble()
            };
        }
    
        public static void WriteBattleViewEventCommandAgentMoveslot(this NetworkWriter writer, PBS.Battle.View.Events.CommandAgent.Moveslot obj)
        {
            writer.WriteString(obj.moveID);
            writer.WriteInt(obj.PP);
            writer.WriteInt(obj.maxPP);
            writer.WriteInt(obj.basePower);
            writer.WriteDouble((double)obj.accuracy);
            writer.WriteBool(obj.hide);
            writer.WriteBool(obj.useable);
            writer.WriteString(obj.failMessageCode);
            writer.WriteList<List<BattlePosition>>(obj.possibleTargets);
        }
        public static PBS.Battle.View.Events.CommandAgent.Moveslot ReadBattleViewEventCommandAgentMoveslot(this NetworkReader reader)
        {
            return new PBS.Battle.View.Events.CommandAgent.Moveslot
            {
                moveID = reader.ReadString(),
                PP = reader.ReadInt(),
                maxPP = reader.ReadInt(),
                basePower = reader.ReadInt(),
                accuracy = (float)reader.ReadDouble(),
                hide = reader.ReadBool(),
                useable = reader.ReadBool(),
                failMessageCode = reader.ReadString(),
                possibleTargets = reader.ReadList<List<BattlePosition>>()
            };
        }
        public static void WriteBattleViewEventCommandAgent(this NetworkWriter writer, PBS.Battle.View.Events.CommandAgent obj)
        {
            writer.WriteString(obj.pokemonUniqueID);
            writer.WriteBool(obj.canMegaEvolve);
            writer.WriteBool(obj.canZMove);
            writer.WriteBool(obj.canDynamax);
            writer.WriteBool(obj.isDynamaxed);
            writer.WriteList(obj.moveslots);
            writer.WriteList(obj.zMoveSlots);
            writer.WriteList(obj.dynamaxMoveSlots);

            List<int> commandInts = new List<int>();
            for (int i = 0; i < obj.commandTypes.Count; i++)
            {
                commandInts.Add((int)obj.commandTypes[i]);
            }
            writer.WriteList(commandInts);
        }
        public static PBS.Battle.View.Events.CommandAgent ReadBattleViewEventCommandAgent(this NetworkReader reader)
        {
            PBS.Battle.View.Events.CommandAgent obj = new PBS.Battle.View.Events.CommandAgent
            {
                pokemonUniqueID = reader.ReadString(),
                canMegaEvolve = reader.ReadBool(),
                canZMove = reader.ReadBool(),
                canDynamax = reader.ReadBool(),
                isDynamaxed = reader.ReadBool(),
                moveslots = reader.ReadList<PBS.Battle.View.Events.CommandAgent.Moveslot>(),
                zMoveSlots = reader.ReadList<PBS.Battle.View.Events.CommandAgent.Moveslot>(),
                dynamaxMoveSlots = reader.ReadList<PBS.Battle.View.Events.CommandAgent.Moveslot>()
            };
            obj.commandTypes = new List<BattleCommandType>();

            List<int> commandInts = reader.ReadList<int>();
            for (int i = 0; i < commandInts.Count; i++)
            {
                obj.commandTypes.Add((BattleCommandType)commandInts[i]);
            }
            return obj;
        }
    }
}
