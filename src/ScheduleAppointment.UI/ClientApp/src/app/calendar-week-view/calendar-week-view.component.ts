import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../app.globals';
import { registerLocaleData } from '@angular/common';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'calendar-week-view',
    providers: [Globals],
    templateUrl: './calendar-week-view.component.html',
    styleUrls: ['./calendar-week-view.component.css']
})
export class CalendarWeekViewComponent
{
    daySlots: DaySlot[];
    private httpClient: HttpClient;
    private globalsApp: Globals;

    facilityId: string;
    namePatient: string;
    surnamesPatient: string;
    emailPatient: string;
    commentsPatient: string;
    phonePatient: number;
    slot: Interval;

    constructor(public dialog: MatDialog, http: HttpClient, private globals: Globals) {

        this.globalsApp = globals;
        this.httpClient = http;

        var monday = this.GetMondayTodayInWeek(new Date(globals.CURRENT_DATE));

        globals.CURRENT_DATE = monday;

        this.FetchDataAPI();
    }

    FetchDataAPI() {
        var mondayFormattedForAPI = this.globalsApp.CURRENT_DATE.getFullYear().toString() +
            ("0" + (this.globalsApp.CURRENT_DATE.getMonth() + 1)).slice(-2) +
            ("0" + (this.globalsApp.CURRENT_DATE.getDate())).slice(-2);

        this.httpClient.get<AvailabilityWeekSlots>(this.globalsApp.API_METHOD_AVAILABILITY_WEEK + mondayFormattedForAPI)
            .subscribe(result => {
                this.facilityId = result.facilityId;
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

    openDialog(selectedSlot): void
    {
        this.slot = selectedSlot;

        let dialogRef = this.dialog.open(TakeSlotFormModal, {
            maxWidth: '250px',
            height:'500px',
            data: {
                facilityId: this.facilityId,
                slot: this.slot,
                namePatient: this.namePatient,
                emailPatient: this.emailPatient,
                surnamesPatient: this.surnamesPatient,
                phonePatient: this.phonePatient,
                commentsPatient: this.commentsPatient
            }
        });

        dialogRef.afterClosed().subscribe(result => {

            if (!result)
                return;

            this.httpClient.post(this.globalsApp.API_METHOD_TAKE_SLOT,
                {
                    FacilityId: result.facilityId,
                    Start: result.slot.start,
                    End: result.slot.end,
                    Comments: result.commentsPatient,
                    Patient:
                    {
                        Email: result.emailPatient,
                        Name: result.namePatient,
                        SecondName: result.surnamesPatient,
                        Phone: result.phonePatient
                    }
                })
                .subscribe(result => {

                    this.FetchDataAPI();

                }, error => console.error(error));

        });
    }
}

interface Interval
{
    start: Date;
    end: Date;
}

interface AvailabilityWeekSlots
{
    facilityId: string;
    consecutiveDaysOfWeek: DaySlot[];
}

interface DaySlot
{
    availableSlots: Interval[];
    currentDate: Date;
}

@Component({
    selector: 'take-slot-form-modal',
    templateUrl: 'take-slot.form.modal.html',
})
export class TakeSlotFormModal
{
    constructor(
        public dialogRef: MatDialogRef<TakeSlotFormModal>,
        @Inject(MAT_DIALOG_DATA) public data: any) { }

    onNoClick(): void {
        this.dialogRef.close();
    }

    private IsValidEmail(email)
    {
        if (email && !email.invalid) {
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email.toLowerCase());
        }

        return false;
    }

    private IsValidNumber(phonePatient)
    {
        if (phonePatient && !phonePatient.invalid && !Number.isNaN(Number(phonePatient)))
            return true;

        return false;
    }

    private IsValidElementForm(elementForm)
    {
        if (elementForm && !elementForm.invalid)
            return true;

        return false;
    }

    IsValidForm(): boolean {
        return (
            this.IsValidEmail(this.data.emailPatient) &&
            this.IsValidNumber(this.data.phonePatient) &&
            this.IsValidElementForm(this.data.namePatient) &&
            this.IsValidElementForm(this.data.commentsPatient) &&
            this.IsValidElementForm(this.data.surnamesPatient)
        );
    }
}