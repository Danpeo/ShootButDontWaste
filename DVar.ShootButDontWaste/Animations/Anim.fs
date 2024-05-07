module DVar.ShootButDontWaste.Animations.Anim

open DVar.ShootButDontWaste.Animations.AnimationTypes

let mushroom (animation: AMushroom) =
  match animation with
  | AMushroom.Hide -> "Hide"
  | AMushroom.Hit -> "Hit"
  | AMushroom.Shoot -> "Shoot"
  | AMushroom.Showup -> "Showup"
  | AMushroom.Squash -> "Squash"
  | AMushroom.Idle -> "Idle"

let player (animation: APlayer) =
  match animation with
  | APlayer.Idle -> "Idle"
  | APlayer.Hit -> "Hit"
  | APlayer.Run -> "Run"
  | APlayer.Shoot -> "Shoot"
  | APlayer.Jump -> "Jump"

let worm (animation: AWorm) =
  match animation with
  | AWorm.Idle -> "Idle"
  | AWorm.Attack -> "Attack"
  | AWorm.Move -> "Move"
  | AWorm.AgroMove -> "AgroMove"
  | AWorm.Hit -> "Hit"
