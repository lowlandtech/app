import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card',
  template: `
    <scx-ui-card-header></scx-ui-card-header>
    <scx-ui-card-body></scx-ui-card-body>
    <scx-ui-card-footer></scx-ui-card-footer>
  `,
  styles: []
})
export class CardComponent implements OnInit {
  @HostBinding('class.card') class1 = true;
  constructor() { }

  ngOnInit(): void {
  }

}
