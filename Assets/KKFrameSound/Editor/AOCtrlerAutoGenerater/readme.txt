音效自动配置器

概述：
主要是简化操作。
原先要配置AudioControler(以下简称AOCtrler)需要手动增加目录;
锁定AOCtrler属性面板;
手动选择音效文件;
再点击"Add Selected audio clips"才算完成。
用这个自动配置器可以自动生成目录，并根据目录结构生成对应的item，subitem.

定义：
首先要明确AOCtrler的几个概念：
  Audio Sub-Item: 一个subitem对应一个音效文件，即对应一个AudioClip资源
  Audio Item: 一个Item可以包含一个Subitem,当执行AOCtrler.Play("")的时候，参数就是item的name;
也可以包含多个subItem,当包含多个的时候调用Play可以随机选择subItem播放，随机方式可以在item的属性中配置。	
  Category: 目录。一个目录可以包含多个item,主要用于方便结构化管理。

使用前：
因为自动配置器是根据目录结构自动生成配置，所以在使用配置器之前先对音效的目录进行设计。
1. 首先是根目录，比如创建一个文件夹为Sound为根目录。在根目录下的每一个文件夹就是一个Category,名称就是文件夹名称，另外处在根目录下的音效文件将被忽略。
2. Category文件下面可以放文件夹也可以直接放置音效文件：
3. 如果放置的是文件夹，则该文件夹将作为一个Item，而该文件夹下面的所有音效文件将作为SubItem
4. 如果方式的是音效文件，则该音效文件就是一个Item，而该Item只有一个SubItem也就是说这个音效文件

使用方法：
1. 打开音效自动配置器
2. 将AOCtrler对象拖拽至面板中位置
3. 选择音效文件夹根目录，比如将之前设计的Sound文件夹选中。注意该目录必须位于Unity工程中。
4. 点击"生成"按钮，可能需要稍等一会，就可以看到AOCtrler属性面板中的所有内容了。

注意：
1. 自动配置器将会对拖入的AOCtrler做清空操作，即如果原来AOCtrler中有数据，则会丢失
