using Platformer.Scripts.Entities.Enemies;
using Platformer.Scripts.State.Common;

namespace Platformer.Scripts.State.WormStates;

public class WormStateHit(Fsm fsm, Worm entity, float waitTime) : StateHit<Worm, WormStatePatrol>(fsm, entity, waitTime);