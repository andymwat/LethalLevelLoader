using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace LethalLevelLoader
{
    public static class Validators
    {
        public static bool ValidateExtendedContent(ExtendedContent extendedContent)
        {
            if (extendedContent is ExtendedLevel extendedLevel)
                return (ValidateExtendedContent(extendedLevel));
            else if (extendedContent is ExtendedDungeonFlow extendedDungeonFlow)
                return (ValidateExtendedContent(extendedDungeonFlow));
            else if (extendedContent is ExtendedItem extendedItem)
                return (ValidateExtendedContent(extendedItem));
            else if (extendedContent is ExtendedEnemyType extendedEnemyType)
                return (ValidateExtendedContent(extendedEnemyType));
            else if (extendedContent is ExtendedFootstepSurface extendedFootstepSurface)
                return (ValidateExtendedContent(extendedFootstepSurface));
            return (false);
        }
        
        public static bool ValidateExtendedContent(ExtendedItem extendedItem)
        {
            bool returnBool = true;

            if (extendedItem == null)
                returnBool = false;
            else if (extendedItem.Item == null)
                returnBool = false;
            else if (extendedItem.Item.spawnPrefab == null)
                returnBool = false;

            return (returnBool);
        }

        public static bool ValidateExtendedContent(ExtendedLevel extendedLevel)
        {
            bool returnBool = true;

            if (extendedLevel == null)
            {
                DebugHelper.LogError($"Null extendedlevel!");
                returnBool = false;
            }
            else if (extendedLevel.selectableLevel == null)
            {
                DebugHelper.LogError($"No selectable level found for {extendedLevel.name}!");
                returnBool = false;
            }
            else if (string.IsNullOrEmpty(extendedLevel.selectableLevel.sceneName))
            {
                DebugHelper.LogError($"No scene name found for {extendedLevel.name}!");
                returnBool = false;
            }
            else if (extendedLevel.selectableLevel.planetPrefab == null)
            {
                DebugHelper.LogError($"No planet prefab found for {extendedLevel.name}!");
                returnBool = false;
            }
            else if (extendedLevel.selectableLevel.planetPrefab.GetComponent<Animator>() == false)
            {
                DebugHelper.LogError($"No animator found for {extendedLevel.name}!");
                returnBool = false;
            }
            else if (extendedLevel.selectableLevel.planetPrefab.GetComponent<Animator>().runtimeAnimatorController == false)
            {
                DebugHelper.LogError($"No animator controller found for {extendedLevel.name}!");
                DebugHelper.LogWarning("TODO HACK");
                //returnBool = true;
            }
            

            return (returnBool);
        }

        public static bool ValidateExtendedContent(ExtendedDungeonFlow extendedDungeonFlow)
        {
            return (true);
        }

        public static bool ValidateExtendedContent(ExtendedEnemyType extendedEnemyType)
        {
            if (extendedEnemyType == null)
                return (false);
            if (extendedEnemyType.EnemyType == null)
                return (false);
            if (extendedEnemyType.EnemyType.enemyPrefab == null)
                return (false);
            if (extendedEnemyType.EnemyType.enemyPrefab.GetComponent<NetworkObject>() == false)
                return (false);
            EnemyAI enemyAI = extendedEnemyType.EnemyType.enemyPrefab.GetComponent<EnemyAI>();
            if (enemyAI == null)
                enemyAI = extendedEnemyType.EnemyType.enemyPrefab.GetComponentInChildren<EnemyAI>();
            if (enemyAI == null)
                return (false);
            if (enemyAI.enemyType == null)
                return (false);
            
            return (true);
        }

        public static bool ValidateExtendedContent(ExtendedWeatherEffect extendedWeatherEffect)
        {
            return (true);
        }

        public static bool ValidateExtendedContent(ExtendedFootstepSurface extendedFootstepSurface)
        {
            return (true);
        }

        public static bool ValidateExtendedContent(ExtendedStoryLog extendedStoryLog)
        {
            if (string.IsNullOrEmpty(extendedStoryLog.sceneName))
                return (false);
            if (string.IsNullOrEmpty(extendedStoryLog.terminalKeywordNoun))
                return (false);
            if (string.IsNullOrEmpty(extendedStoryLog.storyLogTitle))
                return (false);
            if (string.IsNullOrEmpty(extendedStoryLog.storyLogDescription))
                return (false);

            return (true);
        }
    }
}
