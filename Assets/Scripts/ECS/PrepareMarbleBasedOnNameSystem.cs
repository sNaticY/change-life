using Configs;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace ECS
{
    public class PrepareMarbleBasedOnNameSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;
        
        protected override void OnCreate()
        {
            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            
            RequireSingletonForUpdate<MarbleConfigsDataRoot>();
        }

        protected override void OnUpdate()
        {
            var ecb = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            
            var marbleConfigsDataRoot = GetSingleton<MarbleConfigsDataRoot>();
            
            
            Entities.ForEach((Entity teamNameEntity, int entityInQueryIndex, in TeamNameData teamNameData) =>
            {
                //create info for team data
                var marbleInfoForTeamDataEntity = ecb.CreateEntity(entityInQueryIndex);
                ecb.AddComponent(entityInQueryIndex, marbleInfoForTeamDataEntity, PrepMarbleBasedOnName(teamNameData.value));

                //clean
                ecb.DestroyEntity(entityInQueryIndex, teamNameEntity);
            }).ScheduleParallel();
            
            _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(Dependency);

            #region localFunctions

            MarbleInfoForTeamData PrepMarbleBasedOnName(FixedString128 teamName, int marbleAmount = 15)
            {
                return new MarbleInfoForTeamData(
                    marbleConfigsDataRoot.standardMarble,
                    GetRadiusRange(teamName),
                    GetFrictionRange(teamName),
                    GetRestitutionRange(teamName),
                    GetWeightRange(teamName),
                    GetGravityFactorRange(teamName),
                    marbleAmount);
            }

            float2 GetRadiusRange(FixedString128 teamName)
            {
                //the longer the name, the heavier the weight
                return new float2(0.1f, 0.1f);
            }

            float2 GetFrictionRange(FixedString128 teamName)
            {
                return new float2(0.5f, 0.5f);
            }

            float2 GetRestitutionRange(FixedString128 teamName)
            {
                return new float2(0, 0);
            }

            float2 GetWeightRange(FixedString128 teamName)
            {
                return new float2(1, 1);
            }

            float2 GetGravityFactorRange(FixedString128 teamName)
            {
                return new float2(1, 1);
            }

            #endregion
        }
    }
}

public struct TeamNameData : IComponentData
{
    public FixedString128 value;
}