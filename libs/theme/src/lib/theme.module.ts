import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import jss from 'jss';
import preset from 'jss-preset-default';
import { VariablesService } from './variables/variables.service';

jss.setup(preset());

@NgModule({
  imports: [CommonModule],
  providers: [VariablesService]
})
export class ThemeModule { }
