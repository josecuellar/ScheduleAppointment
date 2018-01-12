import { Component, ChangeDetectionStrategy, Inject } from '@angular/core';
import { CalendarEvent, CalendarDateFormatter, DAYS_OF_WEEK } from 'angular-calendar';
import { addDays, addHours, startOfDay, addWeeks, addMonths } from 'date-fns';
import { HttpClient } from '@angular/common/http';

import { CustomDateFormatter } from './custom-date-formatter.provider';


@Component({
    selector: 'calendar-week-view',
    templateUrl: './calendar-week-view.component.html',
    providers: [
        {
            provide: CalendarDateFormatter,
            useClass: CustomDateFormatter
        }
    ]
})
export class CalendarWeekViewComponent
{

    availableWeekSlots: AvailableWeekSlots[];

    viewDate: Date = new Date();

    event: CalendarEvent[] = [
        {
            start: addHours(startOfDay(new Date()), 5),
            end: addHours(startOfDay(new Date()), 17),
            title: 'Event 1',
            color: {
                primary: '#ad2121',
                secondary: '#FAE3E3'
            }
        },
        {
            start: addHours(startOfDay(addDays(new Date(), 1)), 2),
            end: addHours(startOfDay(addDays(new Date(), 1)), 18),
            title: 'Event 2',
            color: {
                primary: '#ad2121',
                secondary: '#FAE3E3'
            }
        }
    ];

    locale: string = 'es';

    weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

    weekendDays: number[] = [DAYS_OF_WEEK.SATURDAY, DAYS_OF_WEEK.SUNDAY];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<AvailableWeekSlots[]>(baseUrl + 'api/availableslots/week').subscribe(result => {
            this.availableWeekSlots = result;
        }, error => console.error(error));
    }

}



interface AvailableWeekSlots {
    Day1: string;
    Day2: string;
    Day3: string;
    Day4: string;
    Day5: string;
    Day6: string;
    Day7: string;
}









