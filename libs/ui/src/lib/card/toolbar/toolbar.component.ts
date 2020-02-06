import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-ui-card-toolbar',
  template: `
    <ng-content></ng-content>
  `,
  styles: []
})
export class ToolbarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
