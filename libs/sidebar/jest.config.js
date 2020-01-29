module.exports = {
  name: 'sidebar',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/sidebar',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
