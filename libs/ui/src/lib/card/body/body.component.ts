import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card-body',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class BodyComponent implements OnInit {
  @HostBinding('class.card-body') class1 = true;
  constructor() { }

  ngOnInit(): void {
  }

}
