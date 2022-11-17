import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})



export class FetchDataComponent {
  public forecasts: any | undefined;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<string>("https://jsonplaceholder.typicode.com/todos/1").subscribe(result => {
      let user = JSON.stringify(result);
      this.forecasts = JSON.parse(user);
      console.log(this.forecasts);
    }, error => console.error(error));
  }
}

interface User {
  userId: number;
  id: number;
  title: string;
  completed: boolean;
}

