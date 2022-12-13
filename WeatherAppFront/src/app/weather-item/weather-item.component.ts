import {Component, EventEmitter, Input, Output} from '@angular/core';
import {CityTemperature} from "./models/cityTemperature";
import {Observable} from "rxjs";

@Component({
  selector: 'app-weather-item',
  templateUrl: './weather-item.component.html',
  styleUrls: ['./weather-item.component.css']
})
export class WeatherItemComponent {
  @Input() weatherItem!: CityTemperature;
  @Output() onDeleteItem = new EventEmitter<CityTemperature>();

  constructor() {
  }

  public deleteItem(item: CityTemperature): void {
    this.onDeleteItem.emit(item);
  }


}
