
import { UiModule } from '../../ui.module';
import { FooterComponent } from './footer.component';

export default {
  title: 'FooterComponent'
}

export const primary = () => ({
  moduleMetadata: {
    imports: []
  },
  component: FooterComponent,
  props: {
  }
})
