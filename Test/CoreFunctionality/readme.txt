controls 
mouse m1 to shoot
move mouse to aim

left shift to dash 

space for shield (unfinish but spawns with timer)

e for interact 
interact list: small circle,center sqaure (door),right most square also needs key ( chest)

enemies
purple guy
shoots stuff that blinds player makes it harder to see

blackguy
follows player and moves faster the lonnger its in los of the player. speed caps out and resets to default speed.    
when touches player it slows player leaving him venerable to enemy attacks

robot guy
moves slowly to player and shoots at player bullets that deal dmg

default enemy1 (looks like plyer)
moves slow right now gonna make los bubble bigger aND	 maybe make it move faster but as of right now will only deal dmg when touches player

small circle thing are placeholder keys 
press e while on it to pickup important for opening the chest 

square on right side of centre point area is place holder chest press e while over it to spawn powerups all i have it dropping right now it pickuphearts which will give player hp 
its really jank right now i have it so that once you press e while you have key it will -1 your current keys you have delete the chest and leave the loot on the ground i might make it so that the chest 
stays fixed in the center area like a hub that the player can goto and cash in keys for rand power ups and stuff 

sqaure in center area is placeholder door which will spawn rand on the map press e while over it to leave room and load new level 

i dont have any checks for player health right now so it can goto neg which means you wont see the heart pickup work because it only adds one rn so if player health is -5 and you pickup a heart youd
still have 0 hearts on the ui for that reason 

from last version to this i worked on added 
rand enemy spawns 
enemy behaviors
score manager that saves high score
proto shield
proto chest door and key
chnaged player ui hearts and score

from this to next goal 
polishing everything: 
shield asset and function
chest asset and better function
door spawning 
key asset and spawning/drop rate 
creating level border /oob
creating pause and gameover state
adding some drops/power ups from chest (no more then 3) 






