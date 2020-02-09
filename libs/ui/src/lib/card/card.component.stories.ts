import {
  text,
  number,
  boolean,
  withKnobs,
  object
} from '@storybook/addon-knobs';
import { storiesOf, moduleMetadata } from '@storybook/angular';
import { CardModule } from './card.module';
import { CardComponent } from './card.component';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';

export default {
  title: '<scx-ui-card>',
  component: 'CardComponent',
  decorators: [
    moduleMetadata({
      imports: [CardModule],
      schemas: [
        CUSTOM_ELEMENTS_SCHEMA,
        NO_ERRORS_SCHEMA
      ]
    }),
    withKnobs
  ]
};

const cardData = {
  id: '1',
  title: 'Card 1',
  key: 'card-1',
  type: 'task',
  tags: ['tag1', 'tag2']
};

storiesOf('@spotacard/ui/<scx-ui-card>', module).add('complete', () => ({
  /* #region template */
  template: `
      <div class="container bg-faded py-3">
        <div class="row">
          <div class="col col-md-12 center">
            <scx-ui-card>
              <scx-ui-card-title>
                Featured
              </scx-ui-card-title>
              <scx-ui-card-toolbar>
                <button class="btn btn-primary btn-sm" type="button">1</button>
                <button class="btn btn-primary btn-sm" type="button">2</button>
              </scx-ui-card-toolbar>
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
        </div>
      </div>
    `,
  /* #endregion */
  component: CardComponent,
  props: {
    card: object('card', { ...cardData })
  }
}));
