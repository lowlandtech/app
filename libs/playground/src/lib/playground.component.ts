import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'scx-playground',
  /* #region template */
  template: `
    <div class="col col-md-12">
      <scx-ui-card>
        <scx-ui-card-header>
          <scx-ui-card-title>
            Featured
          </scx-ui-card-title>
          <scx-ui-card-toolbar>
            <button class="btn btn-primary btn-sm" type="button">1</button>
            <button class="btn btn-primary btn-sm" type="button">2</button>
          </scx-ui-card-toolbar>
        </scx-ui-card-header>
        <scx-ui-card-body>
          <h5 class="card-title">Special title treatment</h5>
          <p class="card-text">
            With supporting text below as a natural lead-in to additional
            content.
          </p>
        </scx-ui-card-body>
        <scx-ui-card-footer>
          <div class="fa-pull-right">
            <a href="#" class="btn btn-primary btn-sm">Go somewhere</a>
          </div>
        </scx-ui-card-footer>
      </scx-ui-card>
      <scx-ui-card>
        <scx-ui-card-header>
          <scx-ui-card-title>
            Featured
          </scx-ui-card-title>
          <scx-ui-card-toolbar>
            <button class="btn btn-primary btn-sm" type="button">3</button>
            <button class="btn btn-primary btn-sm" type="button">4</button>
          </scx-ui-card-toolbar>
        </scx-ui-card-header>
        <scx-ui-card-body>
          <h5 class="card-title">Special title treatment</h5>
          <p class="card-text">
            With supporting text below as a natural lead-in to additional
            content.
          </p>
        </scx-ui-card-body>
        <scx-ui-card-footer>
          <div class="fa-pull-right">
            <a href="#" class="btn btn-primary btn-sm">Go somewhere</a>
          </div>
        </scx-ui-card-footer>
      </scx-ui-card>
    </div>
  `,
  /* #endregion */
  /* #region styles */
  styles: [`
    .col {
      padding-top: 10px;
    }
  `]
  /* #endregion */
})
export class PlaygroundComponent implements OnInit {
  @HostBinding('class.row') class1 = true;
  constructor() {}

  ngOnInit(): void {}
}
