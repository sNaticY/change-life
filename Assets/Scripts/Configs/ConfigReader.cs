using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    public class ConfigReader : MonoBehaviour
    {
        public List<MarbleConfig> marbleConfigs = default;

        #region conversion

        private GameObjectConversionSettings _settings;
        private EntityManager _entityManager;
        
        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            using var bas = new BlobAssetStore();
            _settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, bas);
            
            ConvertMarblesPrefabEntity();
            ReadTeamNameIntoEcs();
        }

        private void ReadTeamNameIntoEcs()
        { 
            var teamNames = ExcelUtil.GetTeamNamesFromCsv(Application.dataPath + "/teamNames.csv");
            foreach (var teamName in teamNames)
            {
                var teamNameEntity = _entityManager.CreateEntity();
                _entityManager.AddComponentData(teamNameEntity, new TeamNameData {value = teamName});
            }
        }

        private void ConvertMarblesPrefabEntity()
        {
            var marbleConfigsDataRoot = new MarbleConfigsDataRoot();
            foreach (var marbleConfig in marbleConfigs)
            {
                marbleConfigsDataRoot.standardMarble = marbleConfig.CreateEntityFromPrefab(_entityManager, _settings);
            }

            var marbleConfigsRootEntity = _entityManager.CreateEntity();
            _entityManager.AddComponentData(marbleConfigsRootEntity, marbleConfigsDataRoot);
        }

        #endregion
        
    }
    
    

    [Serializable]
    public class MarbleConfig
    { 
        [SerializeField] private GameObject marblePrefabEntity;
    
        public Entity CreateEntityFromPrefab(EntityManager em, GameObjectConversionSettings settings)
        {
            return GameObjectConversionUtility.ConvertGameObjectHierarchy(marblePrefabEntity, settings);
        }
    }

    public struct MarbleConfigsDataRoot : IComponentData
    {
        //visual patterns
        public Entity standardMarble;
    }

    public struct MarbleInfoForTeamData : IComponentData
    {
        public readonly Entity marbleEntity;
        public readonly int marbleAmount;

        //physics related
        public readonly float2 radiusMinMax;
        public readonly float2 frictionMinMax;
        public readonly float2 restitutionMinMax;
        public readonly float2 weightMinMax;
        public readonly float2 gravityMinMax;

        public MarbleInfoForTeamData(Entity marbleEntity, float2 radiusMinMax, float2 frictionMinMax, float2 restitutionMinMax, float2 weightMinMax, float2 gravityMinMax, int marbleAmount)
        {
            this.marbleEntity = marbleEntity;
            this.radiusMinMax = radiusMinMax;
            this.frictionMinMax = frictionMinMax;
            this.restitutionMinMax = restitutionMinMax;
            this.weightMinMax = weightMinMax;
            this.gravityMinMax = gravityMinMax;

            this.marbleAmount = marbleAmount;
        }
    }
}
