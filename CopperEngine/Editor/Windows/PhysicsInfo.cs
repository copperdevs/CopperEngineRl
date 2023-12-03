using ImGuiNET;
using JoltPhysicsSharp;

namespace CopperEngine.Editor.Windows;

[EditorWindow("Physics Info", StartingState = false)]
public class PhysicsInfo : BaseEditorWindow
{
    internal override void Render()
    {
        ImGui.DragFloat("Fixed Time Step", ref EnginePhysics.FixedTimeStep);
        ImGui.DragFloat("Fixed Timer", ref EnginePhysics.FixedTimer);
        var simulationGravity = EnginePhysics.Simulation.Gravity;
        if (ImGui.DragFloat3("Gravity", ref simulationGravity))
            EnginePhysics.Simulation.Gravity = simulationGravity;

        var bodiesCount = (int)EnginePhysics.Simulation.PhysicsSystem.BodiesCount;
        ImGui.DragInt("Bodies Count", ref bodiesCount);
        var activeRigidBodies = (int)EnginePhysics.Simulation.PhysicsSystem.GetNumActiveBodies(BodyType.Rigid);
        ImGui.DragInt("Active Rigidbodies", ref activeRigidBodies);
        var activeSoftBodies = (int)EnginePhysics.Simulation.PhysicsSystem.GetNumActiveBodies(BodyType.Soft);
        ImGui.DragInt("Active Soft Bodies", ref activeSoftBodies);
        var maxBodies = (int)EnginePhysics.Simulation.PhysicsSystem.MaxBodies;
        ImGui.DragInt("Max Bodies", ref maxBodies);
    }
}