import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'csx-footer',
  template: `
    <footer class="app-footer">
      <ng-content></ng-content>
    </footer>
  `,
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
