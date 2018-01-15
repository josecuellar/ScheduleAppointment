import { Injectable } from '@angular/core';

@Injectable()
export class Globals
{
    CURRENT_DATE: Date = new Date(Date.now());
    API_METHOD_AVAILABILITY_WEEK: string = 'http://localhost:50821/api/availability/week/';
    API_METHOD_TAKE_SLOT: string = 'http://localhost:50821/api/availability/takeslot/';
}