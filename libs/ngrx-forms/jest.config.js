module.exports = {
  name: 'ngrx-forms',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/ngrx-forms',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
