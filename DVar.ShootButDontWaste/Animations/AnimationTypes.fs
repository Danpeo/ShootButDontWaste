namespace DVar.ShootButDontWaste.Animations.AnimationTypes

[<Struct>]
type AMushroom =
  | Idle
  | Hit
  | Shoot
  | Showup
  | Hide
  | Squash

[<Struct>]
type APlayer =
  | Idle
  | Hit
  | Run
  | Shoot
  | Jump

[<Struct>]
type AWorm =
  | Idle
  | Attack
  | Move
  | AgroMove
  | Hit
