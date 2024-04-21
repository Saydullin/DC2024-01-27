package by.denisova.rest.controller;

import by.denisova.rest.dto.request.CreateStoryDto;
import by.denisova.rest.dto.request.UpdateStoryDto;
import by.denisova.rest.dto.response.StoryResponseDto;
import by.denisova.rest.facade.StoryFacade;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/v1.0/storys")
public class StoryController {

    private final StoryFacade storyFacade;

    @GetMapping("/{id}")
    public StoryResponseDto findStoryById(@PathVariable("id") Long id) {
        return storyFacade.findById(id);
    }

    @GetMapping
    public List<StoryResponseDto> findAll() {
        return storyFacade.findAll();
    }

    @ResponseStatus(HttpStatus.CREATED)
    @PostMapping
    public StoryResponseDto saveStory(@RequestBody @Valid CreateStoryDto storyRequest) {
        return storyFacade.save(storyRequest);
    }

    @ResponseStatus(HttpStatus.NO_CONTENT)
    @DeleteMapping("/{id}")
    public void deleteStory(@PathVariable("id") Long id) {
        storyFacade.delete(id);
    }

    @PutMapping
    public StoryResponseDto updateStory(@RequestBody @Valid UpdateStoryDto storyRequest) {
        return storyFacade.update(storyRequest);
    }
}
