import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  // tslint:disable-next-line:component-selector
  selector: '[page="playground"]',
  templateUrl:'./playground.component.html',
  styleUrls: ['./playground.component.scss']
})
export class PlaygroundComponent implements OnInit {
  @HostBinding('class.row') class1 = true;
  @HostBinding('class.page-playground') class2 = true;
  constructor() {}

  ngOnInit(): void {}
}
