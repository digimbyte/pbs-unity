using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using PBS.Enums.Battle;
using PBS.Main.Pokemon;

namespace PBS.Networking.CustomSerialization
{
    public static class Main
    {
        // Pokemon
        public static void WriteBattleViewCompactPokemon(this NetworkWriter writer, PBS.Battle.View.WifiFriendly.Pokemon obj)
        {
            writer.WriteString(obj.uniqueID);
            writer.WriteString(obj.pokemonID);
            writer.WriteString(obj.nickname);
            writer.WriteInt(obj.teamPos);
            writer.WriteInt(obj.battlePos);
            writer.WriteInt(obj.currentHP);
            writer.WriteInt(obj.maxHP);
            writer.WriteBool(obj.isFainted);
            writer.WriteInt(obj.level);
            writer.WriteInt((int)obj.gender);
            writer.WriteString(obj.nonVolatileStatus);
            writer.WriteInt((int)obj.dynamaxState);
        }
        public static PBS.Battle.View.WifiFriendly.Pokemon ReadBattleViewCompactPokemon(this NetworkReader reader)
        {
            return new PBS.Battle.View.WifiFriendly.Pokemon
            {
                uniqueID = reader.ReadString(),
                pokemonID = reader.ReadString(),
                nickname = reader.ReadString(),
                teamPos = reader.ReadInt(),
                battlePos = reader.ReadInt(),
                currentHP = reader.ReadInt(),
                maxHP = reader.ReadInt(),
                isFainted = reader.ReadBool(),
                level = reader.ReadInt(),
                gender = (PokemonGender)reader.ReadInt(),
                nonVolatileStatus = reader.ReadString(),
                dynamaxState = (Pokemon.DynamaxState)reader.ReadInt()
            };
        }


        // Trainer
        public static void WriteBattleViewCompactTrainer(this NetworkWriter writer, PBS.Battle.View.WifiFriendly.Trainer obj)
        {
            writer.WriteString(obj.name);
            writer.WriteInt(obj.playerID);
            writer.WriteInt(obj.teamPos);
            writer.WriteList(obj.party);
            writer.WriteList(obj.items);
            writer.WriteList(obj.controlPos);
        }
        public static PBS.Battle.View.WifiFriendly.Trainer ReadBattleViewCompactTrainer(this NetworkReader reader)
        {
            return new PBS.Battle.View.WifiFriendly.Trainer
            {
                name = reader.ReadString(),
                playerID = reader.ReadInt(),
                teamPos = reader.ReadInt(),
                party = reader.ReadList<PBS.Battle.View.WifiFriendly.Pokemon>(),
                items = reader.ReadList<string>(),
                controlPos = reader.ReadList<int>()
            };
        }


        // Item
        public static void WriteItem(this NetworkWriter writer, Item item)
        {
            writer.WriteString(item.itemID);
            writer.WriteBool(item.useable);
        }
        public static Item ReadItem(this NetworkReader reader)
        {
            return new Item(
                reader.ReadString(),
                reader.ReadBool());
        }


        // Battle Team
        public static void WriteBattleViewCompactTeam(this NetworkWriter writer, PBS.Battle.View.WifiFriendly.Team obj)
        {
            writer.WriteInt(obj.teamID);
            writer.WriteInt((int)obj.teamMode);
            writer.WriteList(obj.trainers);
        }
        public static PBS.Battle.View.WifiFriendly.Team ReadBattleViewCompactTeam(this NetworkReader reader)
        {
            return new PBS.Battle.View.WifiFriendly.Team
            {
                teamID = reader.ReadInt(),
                teamMode = (TeamMode)reader.ReadInt(),
                trainers = reader.ReadList<PBS.Battle.View.WifiFriendly.Trainer>()
            };
        }


        // Battle
        public static void WriteBattleSettings(this NetworkWriter writer, BattleSettings obj)
        {
            writer.WriteInt((int)obj.battleType);
            writer.WriteBool(obj.isWildBattle);
            writer.WriteBool(obj.isInverse);
            writer.WriteBool(obj.canMegaEvolve);
            writer.WriteBool(obj.canZMove);
            writer.WriteBool(obj.canDynamax);
        }
        public static BattleSettings ReadBattleSettings(this NetworkReader reader)
        {
            return new BattleSettings(
                battleType: (BattleType)reader.ReadInt(),
                isWildBattle: reader.ReadBool(),
                isInverse: reader.ReadBool(),
                canMegaEvolve: reader.ReadBool(),
                canZMove: reader.ReadBool(),
                canDynamax: reader.ReadBool()
                );
        }
    }
}

