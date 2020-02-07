import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card-toolbar',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class ToolbarComponent implements OnInit {
  @HostBinding('class.btn-toolbar') class1 = true;
  @HostBinding('class.justify-content-between') class2 = true;
  @HostBinding('class.float-right') class3 = true;
  @HostBinding('class.card-actions') class4 = true;
  @HostBinding('attr.role') attr1 = 'toolbar';
  @HostBinding('attr.aria-label') attr2 = 'Toolbar with button groups';

  constructor() { }

  ngOnInit(): void {
  }

}
