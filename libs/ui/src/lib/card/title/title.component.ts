import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card-title',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class TitleComponent implements OnInit {
  @HostBinding('class.float-left') class1 = true;
  constructor() { }

  ngOnInit(): void {
  }

}
