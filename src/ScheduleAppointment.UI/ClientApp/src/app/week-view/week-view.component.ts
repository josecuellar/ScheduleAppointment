import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-week-view',
  templateUrl: './week-view.component.html'
})
export class WeekViewComponent {
  public availableWeekSlots: AvailableWeekSlots[];

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
