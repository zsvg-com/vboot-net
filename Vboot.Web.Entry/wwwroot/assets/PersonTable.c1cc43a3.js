import{B as j}from"./TableImg.6db171de.js";import{T as x}from"./BasicForm.4740433f.js";import{u as h}from"./useTable.407be098.js";import{_ as w}from"./index.1f50be75.js";import{A as C,a0 as u,B as k,D as _,w as l,a6 as c,ae as E}from"./vendor.8e08a5be.js";/* empty css                *//* empty css              */import"./useForm.6f5945bb.js";import"./index.4a6d20cf.js";/* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";/* empty css                *//* empty css                *//* empty css               */import"./uuid.2b29000c.js";import"./index.7c24f3ba.js";/* empty css               */import"./useSortable.4b691ef1.js";/* empty css                *//* empty css                *//* empty css               *//* empty css               */import"./index.4765eeb8.js";/* empty css                *//* empty css                *//* empty css                *//* empty css                */import"./download.1020b52a.js";import"./base64Conver.08b9f4ec.js";import"./index.b2563f35.js";const T=[{title:"\u6210\u5458\u59D3\u540D",dataIndex:"name",editRow:!0},{title:"\u5DE5\u53F7",dataIndex:"no",editRow:!0},{title:"\u6240\u5C5E\u90E8\u95E8",dataIndex:"dept",editRow:!0}],g=[{name:"John Brown",no:"00001",dept:"New York No. 1 Lake Park"},{name:"John Brown2",no:"00002",dept:"New York No. 2 Lake Park"},{name:"John Brown3",no:"00003",dept:"New York No. 3Lake Park"}],B=C({components:{BasicTable:j,TableAction:x},setup(){const[t,{getDataSource:o}]=h({columns:T,showIndexColumn:!1,dataSource:g,actionColumn:{width:160,title:"\u64CD\u4F5C",dataIndex:"action",slots:{customRender:"action"}},pagination:!1});function a(e){var n;(n=e.onEdit)==null||n.call(e,!0)}function m(e){var n;if((n=e.onEdit)==null||n.call(e,!1),e.isNew){const i=o(),b=i.findIndex(f=>f.key===e.key);i.splice(b,1)}}function r(e){var n;(n=e.onEdit)==null||n.call(e,!1,!0)}function p(e){console.log(e)}function s(){const e=o(),n={name:"",no:"",dept:"",editable:!0,isNew:!0,key:`${Date.now()}`};e.push(n)}function d(e,n){return e.editable?[{label:"\u4FDD\u5B58",onClick:r.bind(null,e,n)},{label:"\u53D6\u6D88",popConfirm:{title:"\u662F\u5426\u53D6\u6D88\u7F16\u8F91",confirm:m.bind(null,e,n)}}]:[{label:"\u7F16\u8F91",onClick:a.bind(null,e)},{label:"\u5220\u9664"}]}return{registerTable:t,handleEdit:a,createActions:d,handleAdd:s,getDataSource:o,handleEditChange:p}}}),D=E(" \u65B0\u589E\u6210\u5458 ");function F(t,o,a,m,r,p){const s=u("TableAction"),d=u("BasicTable"),e=u("a-button");return k(),_("div",null,[l(d,{onRegister:t.registerTable,onEditChange:t.handleEditChange},{action:c(({record:n,column:i})=>[l(s,{actions:t.createActions(n,i)},null,8,["actions"])]),_:1},8,["onRegister","onEditChange"]),l(e,{block:"",class:"mt-5",type:"dashed",onClick:t.handleAdd},{default:c(()=>[D]),_:1},8,["onClick"])])}var de=w(B,[["render",F]]);export{de as default};
