module Index

open Elmish
open Fable.Remoting.Client
open Shared

type Model = { Todos: Todo list; Input: string }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let model = { Todos = []; Input = "" }

    let cmd =
        Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GotTodos todos -> { model with Todos = todos }, Cmd.none
    | SetInput value -> { model with Input = value }, Cmd.none
    | AddTodo ->
        let todo = Todo.create model.Input

        let cmd =
            Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { model with Input = "" }, cmd
    | AddedTodo todo ->
        { model with
              Todos = model.Todos @ [ todo ] },
        Cmd.none

open Feliz
open Feliz.Bulma

let navBrand =
    Bulma.navbarBrand.div [
        Bulma.navbarItem.a [
            prop.href "https://safe-stack.github.io/"
            navbarItem.isActive
            prop.children [
                Html.img [
                    prop.src "/favicon.png"
                    prop.alt "Logo"
                ]
            ]
        ]
    ]

let containerBox (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            Html.ol [
                for todo in model.Todos do
                    Html.li [ prop.text todo.Description ]
            ]
        ]
        Bulma.field.div [
            field.isGrouped
            prop.children [
                Bulma.control.p [
                    control.isExpanded
                    prop.children [
                        Bulma.input.text [
                            prop.value model.Input
                            prop.placeholder "What needs to be done?"
                            prop.onChange (fun x -> SetInput x |> dispatch)
                        ]
                    ]
                ]
                Bulma.control.p [
                    Bulma.button.a [
                        color.isPrimary
                        prop.disabled (Todo.isValid model.Input |> not)
                        prop.onClick (fun _ -> dispatch AddTodo)
                        prop.text "Add"
                    ]
                ]
            ]
        ]
    ]

let collapsible =
    Html.div [
        prop.id "accordion_first"
        prop.children [
            Html.article [
                prop.className "message"
                prop.children [
                    Html.div [
                        prop.className "message-header"
                        prop.children [
                            Html.p [
                                Html.text "Question 1"
                                Html.a [
                                    prop.custom ("data-action", "collapse")
                                    prop.href "#collapsible-message-accordion-1"
                                    prop.text "Collapse/Expand"
                                ]
                            ]
                        ]
                    ]
                    Html.div [
                        prop.classes [ "message-body"; "is-collapsible" ]
                        prop.custom ("data-parent", "accordion_first")
                        prop.id "collapsible-message-accordion-1"
                        prop.children [
                            Html.div [
                                prop.className "message-body-content"
                                prop.children [
                                    Html.text "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                                    Html.strong "Pellentesque risus mi"
                                    Html.text ", tempus quis placerat ut, porta nec nulla. Vestibulum rhoncus ac ex sit amet fringilla. Nullam gravida purus diam, et dictum "
                                    Html.text " efficitur. Aenean ac "
                                    Html.em "eleifend lacus"
                                    Html.text ", in mollis lectus. Donec sodales, arcu et sollicitudin porttitor, tortor urna tempor ligula, id porttitor mi magna a neque. Donec dui urna, vehicula et sem eget, facilisis sodales sem."
                                ]
                            ]
                        ]
                    ]
                ]
            ]
            Html.article [
                prop.className "message"
                prop.children [
                    Html.div [
                        prop.className "message-header"
                        prop.children [
                            Html.p [
                                Html.text "Question 2 "
                                Html.a [
                                    prop.custom ("data-action", "collapse")
                                    prop.href "#collapsible-message-accordion-2"
                                    prop.text "Collapse/Expand"
                                ]
                            ]
                        ]
                    ]
                    Html.div [
                        prop.classes [ "message-body"; "is-collapsible" ]
                        prop.custom ("data-parent", "accordion_first")
                        prop.id "collapsible-message-accordion-2"
                        prop.children [
                            Html.div [
                                prop.className "message-body-content"
                                prop.children [
                                    Html.text "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                                    Html.strong "Pellentesque risus mi"
                                    Html.text ", tempus quis placerat ut, porta nec nulla. Vestibulum rhoncus ac ex sit amet fringilla. Nullam gravida purus diam, et dictum "
                                    Html.text " efficitur. Aenean ac "
                                    Html.em "eleifend lacus"
                                    Html.text ", in mollis lectus. Donec sodales, arcu et sollicitudin porttitor, tortor urna tempor ligula, id porttitor mi magna a neque. Donec dui urna, vehicula et sem eget, facilisis sodales sem."
                                ]
                            ]
                        ]
                    ]
                ]
            ]
            Html.article [
                prop.className "message"
                prop.children [
                    Html.div [
                        prop.className "message-header"
                        prop.children [
                            Html.p [
                                Html.text "Question 3 "
                                Html.a [
                                    prop.custom ("data-action", "collapse")
                                    prop.href "#collapsible-message-accordion-3"
                                    prop.text "Collapse/Expand"
                                ]
                            ]
                        ]
                    ]
                    Html.div [
                        prop.classes [ "message-body"; "is-collapsible" ]
                        prop.custom ("dataParent", "accordion_first")
                        prop.id "collapsible-message-accordion-3"
                        prop.children [
                            Html.div [
                                prop.className "message-body-content"
                                prop.children [
                                    Html.text "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                                    Html.strong "Pellentesque risus mi"
                                    Html.text ", tempus quis placerat ut, porta nec nulla. Vestibulum rhoncus ac ex sit amet fringilla. Nullam gravida purus diam, et dictum "
                                    Html.text " efficitur. Aenean ac "
                                    Html.em "eleifend lacus"
                                    Html.text ", in mollis lectus. Donec sodales, arcu et sollicitudin porttitor, tortor urna tempor ligula, id porttitor mi magna a neque. Donec dui urna, vehicula et sem eget, facilisis sodales sem."
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]


let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.hero [
        hero.isFullHeight
        color.isPrimary
        prop.style [
            style.backgroundSize "cover"
            style.backgroundImageUrl "https://unsplash.it/1200/900?random"
            style.backgroundPosition "no-repeat center center fixed"
        ]
        prop.children [
            Bulma.heroHead [
                Bulma.navbar [
                    Bulma.container [ navBrand ]
                ]
            ]
            Bulma.heroBody [
                Bulma.container [
                    Bulma.column [
                        column.is6
                        column.isOffset3
                        prop.children [
                            Bulma.title [
                                text.hasTextCentered
                                prop.text "safe_bulma_collapsible"
                                prop.className "make-red"
                            ]
                            collapsible
                        ]
                    ]
                ]
            ]
        ]
    ]
