import{A as m,ba as B,b9 as E,a0 as i,B as C,a1 as g,a6 as a,w as t,ae as p,J as d,z as F,cy as h,b_ as _}from"./vendor.8e08a5be.js";/* empty css               */import{_ as b}from"./index.fa31d1d6.js";import{t as A,a as j,b as k}from"./data.df6b19ce.js";import{P as L}from"./index.4a6d20cf.js";import{_ as R}from"./index.1f50be75.js";import"./useContextMenu.6d6ea44d.js";/* empty css               *//* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";const M=m({components:{BasicTree:b,PageWrapper:L,Row:B,Col:E},setup(){function e(u){console.log(u)}function s(u){return[{label:"\u65B0\u589E",handler:()=>{console.log("\u70B9\u51FB\u4E86\u65B0\u589E",u)},icon:"bi:plus"},{label:"\u5220\u9664",handler:()=>{console.log("\u70B9\u51FB\u4E86\u5220\u9664",u)},icon:"bx:bxs-folder-open"}]}const l=[{render:u=>F(h,{class:"ml-2",onClick:()=>{e(u)}})},{render:()=>F(_)}];function c({level:u}){return u===1?"ion:git-compare-outline":u===2?"ion:home":u===3?"ion:airplane":""}return{treeData:A,treeData2:j,treeData3:k,actionList:l,getRightMenuList:s,createIcon:c}}});function w(e,s,l,c,u,N){const n=i("BasicTree"),r=i("Col"),f=i("Row"),D=i("PageWrapper");return C(),g(D,{title:"Tree\u51FD\u6570\u64CD\u4F5C\u793A\u4F8B"},{default:a(()=>[t(f,{gutter:[16,16]},{default:a(()=>[t(r,{span:8},{default:a(()=>[t(n,{title:"\u53F3\u4FA7\u64CD\u4F5C\u6309\u94AE/\u81EA\u5B9A\u4E49\u56FE\u6807",helpMessage:"\u5E2E\u52A9\u4FE1\u606F",treeData:e.treeData,actionList:e.actionList,renderIcon:e.createIcon},null,8,["treeData","actionList","renderIcon"])]),_:1}),t(r,{span:8},{default:a(()=>[t(n,{title:"\u53F3\u952E\u83DC\u5355",treeData:e.treeData,beforeRightClick:e.getRightMenuList},null,8,["treeData","beforeRightClick"])]),_:1}),t(r,{span:8},{default:a(()=>[t(n,{title:"\u5DE5\u5177\u680F\u4F7F\u7528",toolbar:"",checkable:"",search:"",treeData:e.treeData,beforeRightClick:e.getRightMenuList},null,8,["treeData","beforeRightClick"])]),_:1}),t(r,{span:8},{default:a(()=>[t(n,{title:"\u6CA1\u6709fieldNames\uFF0C\u63D2\u69FD\u6709\u6548",helpMessage:"\u6B63\u786E\u7684\u793A\u4F8B",treeData:e.treeData3},{title:a(o=>[p(" \u63D2\u69FD\uFF1A"+d(o.name),1)]),_:1},8,["treeData"])]),_:1}),t(r,{span:8},{default:a(()=>[t(n,{title:"\u6709fieldNames\u540E\uFF0C\u63D2\u69FD\u5931\u6548",helpMessage:"\u9519\u8BEF\u7684\u793A\u4F8B, \u5E94\u8BE5\u663E\u793A\u63D2\u69FD\u7684\u5185\u5BB9\u624D\u5BF9",fieldNames:{key:"id",title:"name"},treeData:e.treeData2},{title:a(o=>[p(" \u63D2\u69FD\uFF1A"+d(o.title),1)]),_:1},8,["treeData"])]),_:1}),t(r,{span:8},{default:a(()=>[t(n,{title:"\u6709fieldNames\u540E\uFF0CactionList\u5931\u6548",helpMessage:"\u9519\u8BEF\u7684\u793A\u4F8B\uFF0C\u5E94\u8BE5\u5728\u9F20\u6807\u79FB\u4E0A\u53BB\u65F6\uFF0C\u663E\u793A\u65B0\u589E\u548C\u5220\u9664\u6309\u94AE\u624D\u5BF9",treeData:e.treeData3,actionList:e.actionList,fieldNames:{key:"key",title:"name"}},null,8,["treeData","actionList"])]),_:1})]),_:1})]),_:1})}var H=R(M,[["render",w]]);export{H as default};