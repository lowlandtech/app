import { withKnobs } from '@storybook/addon-knobs';
import { centered } from '@storybook/addon-centered/angular';

export const config = {
  title: '@lowlandtech/ui/card',
  decorators: [
    withKnobs,
    centered
  ],
  parameters: {
    knobs: {
      disableDebounce: true
    }
  }
};

export const cardData = {
  id: '1',
  title: 'Card 1',
  key: 'card-1',
  type: 'task',
  tags: ['tag1', 'tag2']
};
