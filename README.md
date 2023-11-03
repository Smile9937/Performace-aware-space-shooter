# Performace-aware-space-shooter

When creating the initial game the performance was not good when spawning 10000 enemies. The biggest reason was that I was using rigidbodies with physics to move the enemies and check for collisions.

The collision checking took around ~50% of the cpu in the profiler. And moving the rigidbodies in fixedupdate also added to the performance dropping.

The game lagged a lot and was consistently below 5 fps.


To improve the performance I made a custom collision script which uses a dictionary with hash sets to separate the bullets from the enemies and then I used aabb to check if the enemies collided with the bullets. I also move them by directly moving the transform in update, This improved the performance massively.

The game now runs between 30 - 60 fps in build depending on if the player is attacking since the enemies now need to check for collision.

I made a solution using dots entities as well but it didn't work in the build of the game.
