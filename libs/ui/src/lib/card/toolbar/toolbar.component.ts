import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-ui-card-toolbar',
  template: `
    <div class="btn-group" role="group" aria-label="First group">
      <ng-content></ng-content>
      <button class="btn btn-primary btn-sm" type="button"><i class="icon-settings"></i></button>
      <button class="btn btn-primary btn-sm" type="button"><i class="icon-arrow-up"></i></button>
      <button class="btn btn-primary btn-sm" type="button"><i class="icon-close"></i></button>
    </div>
  `,
  styles: []
})
export class ToolbarComponent implements OnInit {
  @HostBinding('class.btn-toolbar') class1 = true;
  @HostBinding('class.justify-content-between') class2 = true;
  @HostBinding('class.card-actions') class4 = true;
  @HostBinding('class.pull-right') class5 = true;
  @HostBinding('attr.role') attr1 = 'toolbar';
  @HostBinding('attr.aria-label') attr2 = 'Toolbar with button groups';

  constructor() { }

  ngOnInit(): void {
  }

}
