import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from '../../environments/environment';
import {CityTemperature} from "./models/cityTemperature";

@Injectable({
  providedIn: 'root'
})
export class WeatherItemService {

  constructor(private http: HttpClient) { }

  public getTemperature(zipCode: string, countryCode?: string): Observable<CityTemperature>{
    return this.http.get<CityTemperature>(environment.apiUrl + `WeatherForecast/GetWeatherByZip?zipCode=${zipCode}&countryCode=${countryCode}`)
  }
}
