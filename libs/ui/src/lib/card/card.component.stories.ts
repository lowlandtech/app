import {
  text,
  number,
  boolean,
  withKnobs,
  object
} from '@storybook/addon-knobs';
import { UiModule } from '../ui.module';
import { CardComponent } from './card.component';
import { storiesOf, moduleMetadata } from '@storybook/angular';
import { centered } from '@storybook/addon-centered/angular';

export default {
  title: '<scx-ui-card>',
  component: 'CardComponent',
  decorators: [
    moduleMetadata({
      imports: [UiModule]
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

storiesOf('@spotacard/ui/<scx-ui-card>', module)
  .addDecorator(centered)
  .add('complete', () => ({
    template: `
      <scx-ui-card>
        <div class="card-header-title">
          Featured
        </div>
        <div class="card-toolbar">
          <button class="btn btn-primary btn-sm" type="button">1</button>
          <button class="btn btn-primary btn-sm" type="button">2</button>
        </div>
        <div class="card-body">
          <h5 class="card-title">Special title treatment</h5>
          <p class="card-text">
            With supporting text below as a natural lead-in to additional content.
          </p>
        </div>
        <div class="card-footer">
          <div class="fa-pull-right">
            <a href="#" class="btn btn-primary btn-sm">Go somewhere</a>
          </div>
        </div>
      </scx-ui-card>`,
    component: CardComponent,
    props: {
      card: object('card', { ...cardData })
    }
  }));
