CinQuest
========

A game to help freshers to understand how the Centre of Informatics (CIn) works e what you can do there. It's a gamification of the current Survival Manual also created by the PET-Informática. Each step of the Manual will be transformed into quests of the game. Here is the original [Survival Manual](http://www.cin.ufpe.br/~pet/wiki/index.php/Manual_de_Sobrevivência_do_CIn) available only in Portuguese.

The main objective of the project is to provide a minimal infrastructure for the development of the game and then make it Open Source so the community may be able to collaborate. This way we can offer an opportunity to learn a different platform, encouraging the collaboration and development of a project that the whole CIn community can participate.

![alt text](https://raw.githubusercontent.com/pet-informatica/CinQuest/master/Assets/Sprites/Graphics/cinquest.png "Opening Scene")


BUILD
=====

We're building the game in WebGL. So you need to install the WebGL module into your Unity. If you have already downloaded Unity, you can go download the installer again [here](https://unity3d.com/get-unity/download?ref=personal) and select only the WebGL module (make sure to uncheck all the other options, otherwise you will download everything again unnecessarily).

Once you've done that, in Unity go to File > Build Settings. Make sure WebGL is selected and just click "Build". When it finishes, it'll create a folder with a file named *index.html*. Open this file with Firefox and the game should be working perfectly.