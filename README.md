# Schedule Appointment Challenge

Week view of day available slots and form to booking.

<u>Tech Stack Frontend:</u>
<ul>
    <li><a href="https://angular.io/">Angular 5</a></li>
    <li><a href="https://www.typescriptlang.org/">TypeScript</a></li>
    <li><a href="https://material.angular.io/">Angular Material</a></li>
    <li><a href="https://getbootstrap.com/">Bootstrap</a></li>
    <li><a href="https://www.npmjs.com/">npm</a></li>
</ul>

<u>Tech Stack Backend:</u>
<ul>
    <li><a href="https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.0-download.md">.NET Core 2</a></li>
    <li>
        <a href="https://msdn.microsoft.com/en-us/magazine/mt790184.aspx">C# 7</a>
        <ul>
            <li><a href="https://blogs.msdn.microsoft.com/benjaminperkins/2017/03/23/how-to-enable-c-7-in-visual-studio-2017/">How to Enable to VS2017</a></li>
        </ul>
    
    </li>
    <li><a href="https://swagger.io/">Swagger for API documentation</a></li>
    <li><a href="https://github.com/dotnet/cli">dotNet CLI</a></li>
    
</ul>

<u>Run Demo:</u>
<ul>
    <li>Run frontend:
        <ul>
            <li>Restore packages in root folder: <code><b>\ScheduleAppointment\src\ScheduleAppointment.UI\ClientApp</b></code></li>
            <li>
                <code><b>npm install</b></code>
            </li>
            <li>
                <code><b>npm start</b></code>
            </li>
            <li>Started in: <a href="http://localhost:4200/">http://localhost:4200/</a></li>
        </ul>
    </li>
    <li>Run backend:
        <ul>
            <li>Run dotNet API: <code><b>\ScheduleAppointment\src\ScheduleAppointment.API</b></code></li>
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
UI: <code>\ScheduleAppointment\src\ScheduleAppointment.UI\ClientApp\package.json</code>
</li>
<li>
API: <code>\ScheduleAppointment\src\ScheduleAppointment.API\Properties\launchSettings.json</code>
</li>
<li>
Communication UI 2 API:
<code>\ScheduleAppointment\src\ScheduleAppointment.UI\ClientApp\src\app\app.globals.ts</code>
</li>
</ul>

<u><b>You also can run API and UI using Visual Studio 2017 and <i>F5</i> or <i>Ctrl+F5</i></b></u>
