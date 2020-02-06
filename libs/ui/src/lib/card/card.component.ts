import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  constructor() { }

  ngOnInit(): void {
  }

}
