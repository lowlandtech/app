import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'scx-aside',
  template: `
    <aside class="aside-menu">
      <ng-content></ng-content>
    </aside>
  `,
  styleUrls: ['./aside.component.scss']
})
export class AsideComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
