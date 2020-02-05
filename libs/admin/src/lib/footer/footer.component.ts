import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-footer',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class FooterComponent implements OnInit {
  @HostBinding('class.app-footer') class1 = true;

  constructor() { }

  ngOnInit() {
  }

}
