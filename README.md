# CoachExerciseApp


### How to run localy

#### Step 1: Clone repository
1. Clone repository to your local machine by running  

        git clone https://github.com/AndriyKap/CoachExerciseApp.git

in the chosen directory.


#### Step 2: Update database
1. Change ConnectionStrings in appsettings.json due to your parameters
2. Choose 'CoachExerciseApp.API' as startup project
3. Go to Package Manager Console and set Default project to Infrastructure.API
4. Run command 'Update-Database'
5. If any errors occur, delete the table Exercises and execute step 3 again

#### Step 3: Startup Project
1. Navigate to Solution -> Properties -> Startup Project -> Multiple Startup projects -> Set CoachExerciseApp.API & CoachExerciseApp.UI to Start
2. Apply changes and start the solution
