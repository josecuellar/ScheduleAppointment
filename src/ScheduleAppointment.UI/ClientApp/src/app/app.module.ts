import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { CalendarWeekViewComponent } from './calendar-week-view/calendar-week-view.component';
import { Globals } from './app.globals'

@NgModule({
    declarations: [
        AppComponent,
        CalendarWeekViewComponent
    ],
    providers:[ Globals ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: CalendarWeekViewComponent },
        ])
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }