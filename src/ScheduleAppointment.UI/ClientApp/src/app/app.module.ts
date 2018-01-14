import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CalendarWeekViewComponent, TakeSlotFormModal } from './calendar-week-view/calendar-week-view.component';
import { Globals } from './app.globals'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import {
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    MatToolbarModule,
    MatCardModule,
    MatOptionModule,
    MatSelectModule
} from '@angular/material';

@NgModule({
    declarations: [
        CalendarWeekViewComponent,
        TakeSlotFormModal
    ],
    providers: [Globals],
    entryComponents: [CalendarWeekViewComponent, TakeSlotFormModal],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        BrowserAnimationsModule,
        MatOptionModule,
        MatDialogModule,
        MatFormFieldModule,
        MatMenuModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatToolbarModule,
        MatCardModule,
        HttpClientModule,
        FormsModule,
        MatSelectModule
    ],
    bootstrap: [CalendarWeekViewComponent]
})


export class ScheduleAppointmentModule { }