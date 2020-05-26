import { SharedModule } from '@lowlandtech/shared';
import { object, boolean } from '@storybook/addon-knobs';
import { CardComponent } from '../card.component';
import { cardData, config } from './fixtures';
export default config;

export const Complete = () => {
  const card = object('card', { ...cardData });
  const hasHeader = boolean('has header', true);
  const hasSettings = boolean('has settings', true);
  const sizeable = boolean('sizeable', true);
  const closeable = boolean('closeable', true);
  const hasFooter = boolean('has footer', true);
  const cancelable = boolean('cancelable', true);
  const okable = boolean('okable', true);

  return {
    moduleMetadata: {
      imports: [SharedModule],
      entryComponents: [CardComponent],
      declarations: [CardComponent],
    },
    template: `
      <scx-ui-card
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
          <div class="card-footer-toolbar fa-pull-right">
            <a href="#" class="btn btn-primary btn-sm">Go somewhere</a>
          </div>
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
    },
  };
};

Complete.story = {
  name: 'complete',
};
