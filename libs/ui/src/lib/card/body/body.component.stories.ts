
import { UiModule } from '../../ui.module';
import { BodyComponent } from './body.component';

export default {
  title: 'BodyComponent'
}

export const primary = () => ({
  moduleMetadata: {
    imports: []
  },
  component: BodyComponent,
  props: {
  }
})
