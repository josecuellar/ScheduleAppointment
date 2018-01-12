import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../app.globals';

import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';



@Component({
    selector: 'calendar-week-view',
    providers: [Globals],
    templateUrl: './calendar-week-view.component.html',
    styleUrls: ['./calendar-week-view.component.css']
})
export class CalendarWeekViewComponent {

    daySlots: DaySlot[];
    private httpClient: HttpClient;
    private globalsApp: Globals;

    constructor(http: HttpClient, private globals: Globals) {

        registerLocaleData(localeEs);

        this.globalsApp = globals;
        this.httpClient = http;

        var monday = this.GetMondayTodayInWeek(new Date(globals.CURRENT_DATE));

        globals.CURRENT_DATE = monday;

        this.FetchDataAPI();
    }

    FetchDataAPI()
    {
        var mondayFormattedForAPI = this.globalsApp.CURRENT_DATE.getFullYear().toString() +
            ("0" + (this.globalsApp.CURRENT_DATE.getMonth() + 1)).slice(-2) +
            ("0" + (this.globalsApp.CURRENT_DATE.getDate())).slice(-2);

        this.httpClient.get<AvailabilityWeekSlots>(this.globalsApp.API_METHOD_AVAILABILITY_WEEK + mondayFormattedForAPI)
            .subscribe(result => {
                this.daySlots = result.consecutiveDaysOfWeek;
            }, error => console.error(error));
    }

    NextMonday()
    {
        this.globalsApp.CURRENT_DATE = new Date(this.globalsApp.CURRENT_DATE.setDate(this.globalsApp.CURRENT_DATE.getDate() + 7));
        this.FetchDataAPI();
    }

    PreviousMonday() {

        this.globalsApp.CURRENT_DATE = new Date(this.globalsApp.CURRENT_DATE.setDate(this.globalsApp.CURRENT_DATE.getDate() - 7));
        this.FetchDataAPI();
    }

    GetMondayTodayInWeek(d: Date): Date
    {
        var day = d.getDay(), diff = d.getDate() - day + (day == 0 ? -6 : 1);
        return new Date(d.setDate(diff));
    }
}

interface Interval
{
    start: Date;
    end: Date;
}

interface AvailabilityWeekSlots
{
    consecutiveDaysOfWeek: DaySlot[];
}

interface DaySlot
{
    availableSlots: Interval[];
    currentDate: Date;
}