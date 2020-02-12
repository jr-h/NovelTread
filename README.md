

# NovelTread

![语言](https://img.shields.io/badge/Language-C%23-green.svg)![文件格式](https://img.shields.io/badge/File-txt-lightgrey.svg)

> 一款开源本地小说阅读器

### Project To Do List

- [ ] 基础界面UI
- [ ] 文本读入显示
- [ ] 设置阅读格式
- [ ] 书签、

### Something Useful

首页使用WrapPanel作为书架。默认以`Documents\Novels`文件夹作为库。书签使用`$filename` + `.txt.ini`保存。
每一本书都作为一个`Object`存在，包含书的属性、书签、记录点。 `BookManager`承担管理书本的任务。