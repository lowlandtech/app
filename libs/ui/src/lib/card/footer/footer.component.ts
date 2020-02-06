import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card-footer',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class FooterComponent implements OnInit {
  @HostBinding('class.card-footer') class1 = true;
  constructor() { }

  ngOnInit(): void {
  }

}
