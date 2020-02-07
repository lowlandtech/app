import {
  text,
  number,
  boolean,
  withKnobs,
  object
} from '@storybook/addon-knobs';
import { UiModule } from '../ui.module';
import { CardComponent } from './card.component';

export default {
  title: 'Card',
  component: 'CardComponent',
  decorators: [withKnobs]
};

const cardData = {
  id: '1',
  title: 'Card 1',
  key: 'card-1',
  type: 'task',
  tags: ['tag1', 'tag2']
};

export const complete = () => ({
  name: 'Complete',
  moduleMetadata: {
    imports: [UiModule]
  },
  component: CardComponent,
  /* #region template */
  template: `
  <div class="container bg-faded py-3">
    <div class="row">
        <div class="col center">
          <scx-ui-card>
            <scx-ui-card-header>
              <scx-ui-card-title>
                {{card.title}}
              </scx-ui-card-title>
              <scx-ui-card-toolbar>
                <div class="btn-group" role="group" aria-label="First group">
                  <button class="btn btn-primary btn-sm" type="button">1</button>
                  <button class="btn btn-primary btn-sm" type="button">2</button>
                  <button class="btn btn-primary btn-sm" type="button">3</button>
                  <button class="btn btn-primary btn-sm" type="button">4</button>
                </div>
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
    </div>
  </div>
  `,
  /* #endregion */
  props: {
    card: object('card', { ...cardData })
  }
});
