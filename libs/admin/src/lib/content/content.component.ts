import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-content',
  template: `
    <ng-content></ng-content>
  `
})
export class ContentComponent implements OnInit {
  @HostBinding("class.app-body") class1= true;

  constructor() { }

  ngOnInit() {
  }

}
