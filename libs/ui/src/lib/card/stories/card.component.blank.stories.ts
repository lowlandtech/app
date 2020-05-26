import { SharedModule } from '@lowlandtech/shared';
import { object, boolean } from '@storybook/addon-knobs';
import { CardComponent } from '../card.component';
import { cardData, config } from './fixtures';
export default config;

export const Blank = () => {
  const card = object('card', { ...cardData });
  const hasHeader = boolean('has header', false);
  const hasSettings = boolean('has settings', false);
  const sizeable = boolean('sizeable', false);
  const closeable = boolean('closeable', false);
  const hasFooter = boolean('has footer', false);
  const cancelable = boolean('cancelable', false);
  const okable = boolean('okable', false);

  return {
    moduleMetadata: {
      imports: [SharedModule],
      entryComponents: [CardComponent],
      declarations: [CardComponent]
    },
    template: `
      <scx-ui-card style='min-width: 470px;'
        [card]="card"
        [hasHeader]="hasHeader"
        [hasSettings]="hasSettings"
        [sizeable]="sizeable"
        [closeable]="closeable"
        [hasFooter]="hasFooter"
        [cancelable]="cancelable"
        [okable]="okable"
        >
        <div class="card-header-title">
          {{card.title}}
        </div>
        <div class="card-body">
          <h5 class="card-title">Special title treatment</h5>
          <p class="card-text">
            With supporting text below as a natural lead-in to additional content.
          </p>
        </div>
      </scx-ui-card>`,
    props: {
      card,
      hasHeader,
      hasSettings,
      sizeable,
      closeable,
      hasFooter,
      cancelable,
      okable
    }
  };
};

Blank.story = {
  name: 'blank'
};
