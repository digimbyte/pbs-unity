using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace PBS.Networking.CustomSerialization.Player
{
    public static class Main
    {
        public static void WritePlayerCommand(this NetworkWriter writer, PBS.Player.Command obj)
        {
            writer.WriteInt((int)obj.commandType);
            writer.WriteString(obj.commandUser);
            writer.WriteInt(obj.commandTrainer);
            writer.WriteBool(obj.inProgress);
            writer.WriteBool(obj.completed);
            writer.WriteInt(obj.commandPriority);
            writer.WriteBool(obj.isExplicitlySelected);

            writer.WriteString(obj.moveID);
            writer.WriteBool(obj.consumePP);
            writer.WriteArray(obj.targetPositions);
            writer.WriteBool(obj.displayMove);
            writer.WriteBool(obj.forceOneHit);
            writer.WriteBool(obj.bypassRedirection);
            writer.WriteBool(obj.bypassStatusInterrupt);
            writer.WriteBool(obj.isDanceMove);
            writer.WriteBool(obj.isMoveCalled);
            writer.WriteBool(obj.isMoveReflected);
            writer.WriteBool(obj.isMoveHijacked);
            writer.WriteBool(obj.isFutureSightMove);
            writer.WriteBool(obj.isPursuitMove);
            writer.WriteBool(obj.isMoveSnatched);
            writer.WriteBool(obj.isMagicCoatMove);

            writer.WriteBool(obj.isMegaEvolving);
            writer.WriteBool(obj.isZMove);
            writer.WriteBool(obj.isDynamaxing);

            writer.WriteInt(obj.switchPosition);
            writer.WriteInt(obj.switchingTrainer);
            writer.WriteString(obj.switchInPokemon);

            writer.WriteString(obj.itemID);
            writer.WriteInt(obj.itemTrainer);
        }
        public static PBS.Player.Command ReadPlayerCommand(this NetworkReader reader)
        {
            return new PBS.Player.Command
            {
                commandType = (BattleCommandType)reader.ReadInt(),
                commandUser = reader.ReadString(),
                commandTrainer = reader.ReadInt(),
                inProgress = reader.ReadBool(),
                completed = reader.ReadBool(),
                commandPriority = reader.ReadInt(),
                isExplicitlySelected = reader.ReadBool(),

                moveID = reader.ReadString(),
                consumePP = reader.ReadBool(),
                targetPositions = reader.ReadArray<BattlePosition>(),
                displayMove = reader.ReadBool(),
                forceOneHit = reader.ReadBool(),
                bypassRedirection = reader.ReadBool(),
                bypassStatusInterrupt = reader.ReadBool(),
                isDanceMove = reader.ReadBool(),
                isMoveCalled = reader.ReadBool(),
                isMoveReflected = reader.ReadBool(),
                isMoveHijacked = reader.ReadBool(),
                isFutureSightMove = reader.ReadBool(),
                isPursuitMove = reader.ReadBool(),
                isMoveSnatched = reader.ReadBool(),
                isMagicCoatMove = reader.ReadBool(),

                isMegaEvolving = reader.ReadBool(),
                isZMove = reader.ReadBool(),
                isDynamaxing = reader.ReadBool(),

                switchPosition = reader.ReadInt(),
                switchingTrainer = reader.ReadInt(),
                switchInPokemon = reader.ReadString(),

                itemID = reader.ReadString(),
                itemTrainer = reader.ReadInt()
            };
        }
    }
}
