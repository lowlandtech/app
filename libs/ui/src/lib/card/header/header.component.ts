import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card-header',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class HeaderComponent implements OnInit {
  @HostBinding('class.card-header') class1 = true;
  constructor() { }

  ngOnInit(): void {
  }

}
