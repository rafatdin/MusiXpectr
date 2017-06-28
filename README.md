# MUSIXPECTR
				Developing More Interactive Game to Gain Higher Player Retention
_A project undertaken as part of BSc (Hons) Business Information Systems Degree, Westminster_


by	**Rafatdin Shimbergenov**

>In accordance with University regulations, Final Year Project (both software and report) is the UoW property. The incomplete content of the project is provided to you for reference only.

## Acknowledgements
I would like to take this opportunity to tender my sincere gratitude to BIS faculty members – Vasiliy Kuznetsov, Temur Batirov and Farrukh Normuradov. Their timely and scholarly advices have helped me to a very great extent to accomplish the task.

In addition, I would like to thank Dylan Fitterer for an amazing game Audiosurf 2 that has inspired me on the idea of applying the digital signal processing and Fourier analysis into the game.

I would also like to thank the Un4seen and Unity Technologies for providing their products, documentation and tutorials that were vital during the project implementation. Finally, I thank my BIS fellow course mates’ community, friends and family for their support.

## Contents
### Acknowledgements


### Abstract


### 1	Introduction

<details>
<summary>2. Literature review</summary>

  2.1. Mobile video gaming industry


  2.2. Monetization
  
  2.3. Customer retention strategy
  
  2.4. Digital signal processing
</details>

<details>
<summary>3. Business Area Analysis</summary>
 3.1.	Industry Analysis
 
 3.1.1 Threat of new entrants
 
 3.1.2 Bargaining power of customers
 
 3.1.3 Bargaining power of suppliers
 
 3.1.4 Threat of substitutes
 
 3.1.5 Intensity of competitive rivalry
 
 3.2 Competitors’ analysis
 
 3.3 Monetization
 
 3.4 SWOT analysis
 
</details>

<details>
<summary>4. IT strategy</summary>
4.1 Software Justification

4.1.1 Platform

4.1.2 Gaming Engine

4.1.3 Programming Language &amp; Environment

4.1.4 Music Importing Library

4.2 Methodology

4.3 Implementation

4.4 Testing
</details>

<details>
<summary>5. Results</summary>
5.1 Prototype #1

5.2 Prototype #4

5.3 Sixth prototype

5.4 Programming skills
</details>


6. Conclusions
###	7 Final Chapter
----
In this chapter I would like to make the critical self-evaluation on the work that has been
done, as well as possible further development of the project.

The idea for the project was born in the end of academic year 2014-2015, when I was
finishing level 5 and was looking at the final projects of graduates. The most interesting and
exciting ones were the games, however hardly any of those games could be competitive
enough to succeed on the market, because games’ concepts were not innovative and/or of
poor quality.

Obviously, as an independent developer, one cannot present AAA quality mobile game
within such short time and in scope of final year project. In addition, at some point it was
clear that the project would not exactly follow the initial plans and research scope and
requirements has changed and evolved over time. Still, it cannot be said that any dramatic
alterations were made: the provisional title of project provided in the original proposal was
“Making more interactive videogame using players’ music library” in the area of “mobile
video gaming”. Meaning, it was clear in the very initiation of the project **_what_** it should
deliver, **_the problem_** it is going to solve and *__how__* the objectives would be reached.

Dividing the project into tasks and identifying credible deadline for accomplishing those
was a good practice in following the methodology of projects execution. Each task was a
milestone by which the application would have certain feature implemented. Based on the
experience up to this time such approach is leaving me very small room for possibility of
running out of time, given that task was plausible enough to implement and I would not overestimate my skills.

Hardly any project goes smooth and has no issues at all. This project is not an exception. The major issue encountered in this work was finding and implementing the suitable library for audio file importing into the game. Most libraries provided on the web which would work on android devices were written on Java and not compatible with .NET, which made me to stop the project and switch the game engine to libGDX, which is open source and cross

platform engine that supports the Java programming language. There were performance
problems with project migration from Unity3d to new engine, and considering how much
work has already been done up to that time, starting the project over again was not an
option. The search of appropriate library took about two weeks, until I was suggested to
use the BASS Audio Library. The usage documentation provided by the founders was very
poor and it took one more week to implement it in the project.

This was the major issue, which made the project lag for about the month behind initially
planned schedule. Besides, for the student who used to have independent learning weeks
the previous three years, the university’s new policy of shortened academic year has stolen
invaluable time for further research: implementing the monetization and testing the
efficiency of developed strategy for player retention. This was considered as main
component of current research, not only the main limitation of the project.

Other opportunities include the development of game for other platforms – with some
modifications – Unity lets to compile the game for Windows Phone and iOS devices. This
way the project would target almost entire mobile gamers around the world.

Moreover, as the software was developed solely, all components of application – system
design, 3D models and environment design, scripting – had to be done by me. This was
my main mistake, which resulted in poorer quality of the game than expected. If there’s one
thing I would do differently – is outsource the development of sprite sheets, buttons,
images, logo and other related design documents to professionals.

Finally, as main weakness of the game it should be stated now only small amount of the available data is used for game effects. The more objects and events might be added to synchronize with tempo and beats of the music to dramatically improve the gameplay experience.

-------
8. List of references
