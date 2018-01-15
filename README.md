# Schedule Appointment Challenge

Week view of day available slots and form to booking.

<h3>UI</h3>
<img width="800" src="https://github.com/josecuellar/ScheduleAppointment/blob/master/ReadmeImage2.jpg?raw=true" />

<h3>API</h3>
<img width="800" src="https://github.com/josecuellar/ScheduleAppointment/blob/master/ReadmeImage1.jpg?raw=true" />

<h4>Tech Stack Frontend:</h4>
<ul>
    <li><a href="https://angular.io/">Angular 5</a></li>
    <li><a href="https://www.typescriptlang.org/">TypeScript</a></li>
    <li><a href="https://material.angular.io/">Angular Material</a></li>
    <li><a href="https://getbootstrap.com/">Bootstrap</a></li>
    <li><a href="https://www.npmjs.com/">npm</a></li>
</ul>

<h4>Tech Stack Backend:</h4>
<ul>
    <li><a href="https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.0-download.md">.NET Core 2</a></li>
    <li>
        <a href="https://msdn.microsoft.com/en-us/magazine/mt790184.aspx">C# 7 (<a href="https://blogs.msdn.microsoft.com/benjaminperkins/2017/03/23/how-to-enable-c-7-in-visual-studio-2017/">How to Enable to VS2017</a>)</a>
    </li>
    <li><a href="https://swagger.io/">Swagger for API documentation</a></li>
    <li><a href="https://github.com/dotnet/cli">dotNet CLI</a></li>
    <li>Tests with <a href="https://github.com/nunit/docs/wiki/.NET-Core-and-.NET-Standard">NUnit</a> & <a href="https://github.com/moq/moq4">Moq</a><br>
    <img width="650" src="https://github.com/josecuellar/ScheduleAppointment/blob/master/ReadmeImage3.jpg?raw=true">

</ul>

<h4>Requirements to run:</h4>
<ul>
    <li>NPM</li>
    <li>Angular CLI</li>
    <li>.Net Core CLI</li>
    <li>C# 7</li>
    <li>.Net Core 2</li>
</ul>
        

<h4>Run Demo:</h4>
<ul>
    <li>Run frontend:
        <ul>
            <li>Go to root folder: <code><b>src\ScheduleAppointment.UI\ClientApp</b></code></li>
            <li>
                <code><b>npm install</b></code>
            </li>
            <li>
                <code><b>npm start</b></code>
            </li>
            <li>Started in: <a href="http://localhost:4201/">http://localhost:4201/</a></li>
        </ul>
    </li>
    <li>Run backend:
        <ul>
            <li>Go to root folder: <code><b>src\ScheduleAppointment.API</b></code></li>
            <li>
                <code><b>dotnet run</b></code>
            </li>
            <li>Started in <a href="http://localhost:50821/">http://localhost:50821/</a></li>
        </ul>
    </li>
</ul>

<u>If you have problems with local port permissions, you can change configuration ports used in:</u>
<ul>
<li>
UI: <code>src\ScheduleAppointment.UI\ClientApp\package.json</code>
</li>
<li>
API: <code>src\ScheduleAppointment.API\Properties\launchSettings.json</code>
</li>
<li>
Communication UI 2 API:
<code>src\ScheduleAppointment.UI\ClientApp\src\app\app.globals.ts</code>
</li> 
</ul>

<u><b>You also can run API and UI using Visual Studio 2017 and <i>F5</i> or <i>Ctrl+F5</i> to both projects</b></u>

<i>*ToDos:</i> <br>
<i>*Tests in frontend with Jasmine || Mocha & Karma.</i><br>
<i>*Handle client exceptions and show frindly user error message.</i>
<i>*Organize available slots with segments of hours in all days.</i>