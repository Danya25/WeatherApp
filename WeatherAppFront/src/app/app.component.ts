import {Component} from '@angular/core';
import {WeatherItemService} from "./weather-item/weather-item.service";
import {CityTemperature} from "./weather-item/models/cityTemperature";
import {filter, Observable} from "rxjs";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'WeatherAppFront';
  public myForm: FormGroup = new FormGroup({
    zipCode: new FormControl(null, Validators.required),
    countryCode: new FormControl('us', Validators.required)
  })

  // Observable possible
  public weathers: CityTemperature[] = [];

  constructor(private weatherService: WeatherItemService) {
  }

  public addWeather() {
    const zipCode = this.myForm.controls['zipCode'].value;
    const countryCode = this.myForm.controls['countryCode'].value;
    // Observable possible
    this.weatherService.getTemperature(zipCode, countryCode).subscribe(t => this.weathers.push(t));
  }

  public deleteWeather($event: CityTemperature) {
    this.weathers = this.weathers.filter(t => t !== $event);
  }
}
